using AutoMapper;
using GMAO.Application.Common.Exceptions;
using GMAO.Application.Common.Interfaces.Repositories;
using GMAO.Application.Common.Interfaces.Services.Business;
using GMAO.Application.Features.sites.Commands.assets.CreateAsset;
using GMAO.Application.Features.sites.Commands.assets.UpdateAsset;
using GMAO.Application.Features.sites.Commands.CreateSite;
using GMAO.Application.Features.sites.Commands.Occupants.CreateOccupant;
using GMAO.Application.Features.sites.Commands.Occupants.UpdateOccupant;
using GMAO.Application.Features.sites.Commands.SiteContacts.CreateSiteContact;
using GMAO.Application.Features.sites.Commands.SiteContacts.UpdateSiteContact;
using GMAO.Application.Features.sites.Commands.SiteKeeper.CreateSiteKeeper;
using GMAO.Application.Features.sites.Commands.SiteKeeper.UpdateSiteKeeper;
using GMAO.Application.Features.sites.Commands.Units.CreateUnit;
using GMAO.Application.Features.sites.Commands.Units.UpdateUnit;
using GMAO.Application.Features.sites.Commands.UpdateSite;
using GMAO.Domain.Entities;
using GMAO.Domain.Entities.Auth;
using GMAO.Domain.Entities.siteAggregate;
using GMAO.Domain.Enums;
using GMAO.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.DomainService.Business
{
    public class SiteServiceAsync : ISiteServiceAsync
    {
        private readonly ISiteRepositoryAsync _siteRepository;
        private readonly IRepository<SectorType> _repoSectorType;
        private readonly IRepository<Domain.Entities.PaymentMethod> _repoPaymentMethod;
        private readonly IRepository<SiteClientType> _repoClientType;
        private readonly IRepository<Staff> _repoStaff;
        private readonly IRepository<TenantUser> _repoTenantUser;
        private readonly IRepository<TVA> _repoTva;
        private readonly IRepository<Customer> _repoCustomer;
        private readonly IRepository<CustomerContact> _repoCustomerContact;
        private readonly IRepository<Unit> repoUnit;
        private readonly IRepository<Occupant> occupantRepo;
        private readonly IRepository<Asset> assetRepo;
        private readonly IRepository<AssetCategory> repoAssetCategory;
        private readonly IRepository<SiteKeeper> repoSiteKeeper;
        private readonly IRepository<SiteContact> repoSiteContact;
        private readonly IRepository<Occupant> repoOccupant;
        private readonly IRepository<ContactType> repoContactType;
        private readonly IMapper mapper;

        public SiteServiceAsync(ISiteRepositoryAsync siteRepository,
                                IRepository<SectorType> repoSectorType,
                                IRepository<Domain.Entities.PaymentMethod> repoPaymentMethod,
                                IRepository<SiteClientType> repoClientType,
                                IRepository<Staff> repoStaff,
                                IRepository<TenantUser> repoTenantUser,
                                IRepository<TVA> repoTva,
                                IRepository<Customer> customer,
                                IRepository<CustomerContact> repoCustomerContact,
                                IRepository<Unit> repoUnit,
                                IRepository<Occupant> occupantRepo,
                                IRepository<Asset> assetRepo,
                                IRepository<AssetCategory> _repoAssetCategory,
                                IRepository<SiteKeeper> repoSiteKeeper,
                                IRepository<SiteContact> repoSiteContact,
                                IRepository<Occupant> repoOccupant,
                                IRepository<ContactType> repoContactType,
                                IMapper mapper)
        {
            this._siteRepository = siteRepository;
            this._repoSectorType = repoSectorType;
            this._repoPaymentMethod = repoPaymentMethod;
            this._repoClientType = repoClientType;
            this._repoStaff = repoStaff;
            this._repoTenantUser = repoTenantUser;
            this._repoTva = repoTva;
            this._repoCustomer = customer;
            this._repoCustomerContact = repoCustomerContact;
            this.repoUnit = repoUnit;
            this.occupantRepo = occupantRepo;
            this.assetRepo = assetRepo;
            repoAssetCategory = _repoAssetCategory;
            this.repoSiteKeeper = repoSiteKeeper;
            this.repoSiteContact = repoSiteContact;
            this.repoOccupant = repoOccupant;
            this.repoContactType = repoContactType;
            this.mapper = mapper;
        }
        #region Site Management
        public async Task<Guid> CreateSiteAsync(CreateSiteCommand request, CancellationToken cancellationToken)
        {
            await ValidateCreateRequestAsync(request, cancellationToken);

            var site = mapper.Map<Site>(request);
            site.Id = Guid.NewGuid();
            await _siteRepository.AddAsync(site, cancellationToken);
            await _siteRepository.SaveChangesAsync(cancellationToken);
            return site.Id;
        }

        public async Task<bool> UpdateSiteAsync(UpdateSiteCommand request, CancellationToken cancellationToken)
        {
            await ValidateUpdateRequestAsync(request, cancellationToken);

            var site = await _siteRepository.GetByIdAsync(request.Id, cancellationToken);

            mapper.Map(request, site);
            site!.Id = request.Id;
            _siteRepository.Update(site!);
            await _siteRepository.SaveChangesAsync(cancellationToken);
            return true;
        }


        /// <summary>
        /// Valide les clés étrangères communes à Create et Update
        /// </summary>
        private async Task<Dictionary<string, string[]>> ValidateForeignKeysAsync(
            Guid customerId,
            Guid? sectorTypeId,
            Guid? clientTypeId,
            Guid? vatId,
            Guid? paymentMethodId,
            Guid? clientContactId,
            Guid? commercialId,
            Guid? operationsManagerId,
            Guid? sectorManagerId,
            Guid? technician1Id,
            Guid? technician2Id,
            CancellationToken cancellationToken = default)
        {
            var errors = new Dictionary<string, string[]>();

            // Customer (obligatoire)
            var customerExists = await _repoCustomer.AnyAsync(c => c.Id == customerId, cancellationToken);
            if (!customerExists)
            {
                errors.Add("CustomerId", ["Le client spécifié n'existe pas"]);
            }

            // Référentiels optionnels
            if (sectorTypeId.HasValue && !await _repoSectorType.AnyAsync(r => r.Id == sectorTypeId.Value, cancellationToken))
                errors.Add("SectorTypeId", ["Le type de secteur spécifié n'existe pas"]);

            if (clientTypeId.HasValue && !await _repoClientType.AnyAsync(r => r.Id == clientTypeId.Value, cancellationToken))
                errors.Add("ClientTypeId", ["Le type de client spécifié n'existe pas"]);

            if (vatId.HasValue && !await _repoTva.AnyAsync(r => r.Id == vatId.Value, cancellationToken))
                errors.Add("VatId", ["Le taux de TVA spécifié n'existe pas"]);

            if (paymentMethodId.HasValue && !await _repoPaymentMethod.AnyAsync(r => r.Id == paymentMethodId.Value, cancellationToken))
                errors.Add("PaymentMethodId", ["Le mode de paiement spécifié n'existe pas"]);

            // Contact client
            if (clientContactId.HasValue)
            {
                var contactExists = await _repoCustomerContact.AnyAsync(
                    c => c.Id == clientContactId.Value && c.CustomerId == customerId, cancellationToken);
                if (!contactExists)
                    errors.Add("ClientContactId", ["Le contact spécifié n'existe pas ou n'appartient pas au client sélectionné"]);
            }

            // Équipe Staff - Optimisé
            var staffIdsToValidate = new List<(Guid Id, string PropertyName, string DisplayName)>();

            if (commercialId.HasValue)
                staffIdsToValidate.Add((commercialId.Value, "CommercialId", "Le commercial"));
            if (operationsManagerId.HasValue)
                staffIdsToValidate.Add((operationsManagerId.Value, "OperationsManagerId", "Le responsable d'exploitation"));
            if (sectorManagerId.HasValue)
                staffIdsToValidate.Add((sectorManagerId.Value, "SectorManagerId", "Le chef de secteur"));
            if (technician1Id.HasValue)
                staffIdsToValidate.Add((technician1Id.Value, "Technician1Id", "Le technicien principal"));
            if (technician2Id.HasValue)
                staffIdsToValidate.Add((technician2Id.Value, "Technician2Id", "Le technicien secondaire"));

            if (staffIdsToValidate.Any())
            {
                var staffIds = staffIdsToValidate.Select(s => s.Id).Distinct().ToList();

                // Une seule requête pour tous les Staff
                var existingStaffIds = await _repoStaff
                    .FilterAsync(s => staffIds.Contains(s.Id), cancellationToken);

                var existingStaffIdSet = existingStaffIds.Select(s => s.Id).ToHashSet();


                foreach (var (id, propertyName, displayName) in staffIdsToValidate)
                {
                    if (!existingStaffIdSet.Contains(id))
                    {
                        errors.Add(propertyName, [$"{displayName} spécifié n'existe pas"]);
                    }
                }
            }

            return errors;
        }

        // Utilisation dans Create
        private async Task ValidateCreateRequestAsync(CreateSiteCommand request, CancellationToken cancellationToken)
        {
            var errors = await ValidateForeignKeysAsync(
                request.CustomerId,
                request.SectorTypeId,
                request.ClientTypeId,
                request.VatId,
                request.PaymentMethodId,
                request.ClientContactId,
                request.CommercialId,
                request.OperationsManagerId,
                request.SectorManagerId,
                request.Technician1Id,
                request.Technician2Id,
                cancellationToken);

            // Unicité référence (nouveau site)
            if (await _siteRepository.AnyAsync(s => s.Reference == request.Reference, cancellationToken))
                errors.Add(nameof(request.Reference), ["Cette référence de site existe déjà"]);

            if (errors.Count > 0)
                throw new AppValidationException(errors);
        }

        // Utilisation dans Update
        private async Task ValidateUpdateRequestAsync(UpdateSiteCommand request, CancellationToken cancellationToken)
        {
            // Vérifier existence du site
            if (!await _siteRepository.AnyAsync(s => s.Id == request.Id, cancellationToken))
                throw new NotFoundException(nameof(Site), request.Id);

            var errors = await ValidateForeignKeysAsync(
                request.CustomerId,
                request.SectorTypeId,
                request.ClientTypeId,
                request.VatId,
                request.PaymentMethodId,
                request.ClientContactId,
                request.CommercialId,
                request.OperationsManagerId,
                request.SectorManagerId,
                request.Technician1Id,
                request.Technician2Id,
                cancellationToken);

            // Unicité référence (exclure le site actuel)
            if (await _siteRepository.AnyAsync(s => s.Reference == request.Reference && s.Id != request.Id, cancellationToken))
                errors.Add(nameof(request.Reference), ["Cette référence de site est déjà utilisée par un autre site"]);

            if (errors.Count > 0)
                throw new AppValidationException(errors);
        }

        #endregion


        #region Unit Management
        private async Task ValidateAddUnit(CreateUnitCommand request)
        {
            var errors = new Dictionary<string, string[]>();

            var siteExist = await _siteRepository.AnyAsync(s => request.SiteId == s.Id);

            if (!siteExist)
            {
                errors.Add(nameof(request.SiteId), ["Le site spécifié n'existe pas"]);
            }

            var unitExists = await repoUnit.AnyAsync(u => u.Reference == request.Reference && u.SiteId == request.SiteId);
            // Unicité de la référence dans le site
            if (unitExists)
            {
                errors.Add(nameof(request.Reference), ["Cette référence existe déjà pour ce site"]);
            }

            // Unicité du numéro de porte sur le même étage
            if (!string.IsNullOrEmpty(request.DoorNumber))
            {
                var doorExists = await repoUnit.AnyAsync(u =>
                    u.Floor == request.Floor &&
                    u.DoorNumber == request.DoorNumber);

                if (doorExists)
                {
                    errors.Add(nameof(request.DoorNumber), ["Ce numéro de porte existe déjà sur cet étage"]);
                }
            }

            // Validation des tantièmes (invariant de l'agrégat)
            //ValidateOwnershipShares(site, null, request.OwnershipSharesCount, errors); pas pour maintenant

            if (errors.Count > 0)
                throw new AppValidationException(errors);
        }
        public async Task<Guid> AddUnitAsync(CreateUnitCommand unitCommand, CancellationToken cancellationToken)
        {
            await ValidateAddUnit(unitCommand);
            var unit = mapper.Map<Unit>(unitCommand);
            await repoUnit.AddAsync(unit, cancellationToken);
            await repoUnit.SaveChangesAsync(cancellationToken);
            return unit.Id;
        }
        private async Task ValidateUpdateUnit(UpdateUnitCommand request)
        {
            var errors = new Dictionary<string, string[]>();
            var siteExist = await _siteRepository.AnyAsync(s => request.SiteId == s.Id);
            if (!siteExist)
                errors.Add(nameof(request.SiteId), ["Le site spécifié n'existe pas"]);

            // Unicité de la référence (exclure l'unité actuelle)
            if (await repoUnit.AnyAsync(u => u.Reference == request.Reference && u.Id != request.Id && u.SiteId == request.SiteId))
                errors.Add(nameof(request.Reference), ["Cette référence existe déjà pour ce site"]);

            // Unicité du numéro de porte (exclure l'unité actuelle)
            if (!string.IsNullOrEmpty(request.DoorNumber))
            {
                var doorExists = await repoUnit.AnyAsync(u =>
                    u.Floor == request.Floor &&
                    u.DoorNumber == request.DoorNumber &&
                    u.Id != request.Id
                    && u.SiteId == request.SiteId);

                if (doorExists)
                    errors.Add(nameof(request.DoorNumber), ["Ce numéro de porte existe déjà sur cet étage"]);
            }
            // Validation des tantièmes (invariant de l'agrégat)
            //ValidateOwnershipShares(site, request.Id, request.OwnershipSharesCount, errors); pas pour maintenant

            // Vérifier qu'on ne peut pas passer en "Vacant" si occupants actifs
            if (request.Status == UnitStatus.Vacant)
            {
                var hasActiveOccupants = await occupantRepo.AnyAsync(o => o.MoveOutDate == null && o.UnitId == request.Id);
                if (hasActiveOccupants)
                    errors.Add(nameof(request.Status), ["Impossible de passer en 'Vacant' : le lot a des occupants actifs"]);
            }

            if (errors.Count > 0)
                throw new AppValidationException(errors);
        }
        public async Task<Guid> UpdateUnitAsync(UpdateUnitCommand unitCommand, CancellationToken cancellationToken)
        {
            var unit = await repoUnit.GetByIdAsync(unitCommand.Id, cancellationToken);
            if (unit == null)
                throw new NotFoundException(nameof(Unit), unitCommand.Id);

            await ValidateUpdateUnit(unitCommand);

            mapper.Map(unitCommand, unit);
            repoUnit.Update(unit);
            await repoUnit.SaveChangesAsync(cancellationToken);
            return unit.Id;
        }

        #endregion


        #region ASSET MANAGEMENT

        public async Task<Guid> AddAssetAsync(CreateAssetCommand request, CancellationToken cancellationToken)
        {
            // 1. Valider les clés étrangères externes à l'agrégat
            await ValidateAssetForeignKeysAsync(request.CategoryId, request.SiteId, request.UnitId, cancellationToken);

            // 2. Valider via l'agrégat (invariants métier)
            await ValidateAddAsset(request);

            // 3. Créer l'équipement
            var asset = mapper.Map<Asset>(request);
            asset.AssetCategoryId = request.CategoryId;

            await assetRepo.AddAsync(asset);

            await assetRepo.SaveChangesAsync(cancellationToken);

            return asset.Id;
        }
        public async Task<Guid> UpdateAssetAsync(UpdateAssetCommand request, CancellationToken cancellationToken)
        {
            var asset = await assetRepo.GetByIdAsync(request.Id, cancellationToken);

            if (asset == null)
                throw new NotFoundException(nameof(Asset), request.SiteId);

            // 3. Valider les clés étrangères externes à l'agrégat
            await ValidateAssetForeignKeysAsync(request.CategoryId, request.SiteId, request.UnitId, cancellationToken);

            // 4. Valider via l'agrégat (invariants métier)
            await ValidateUpdateAsset(request);

            // 5. Mettre à jour l'équipement
            asset.Reference = request.Reference;
            asset.Name = request.Name;
            asset.AssetCategoryId = request.CategoryId;
            asset.UnitId = request.UnitId;
            asset.IsCommonAsset = request.IsCommonAsset;
            asset.Location = request.Location != null ? new AssetLocation
            {
                PlanDocumentId = request.Location.PlanDocumentId,
                XPosition = request.Location.XPosition,
                YPosition = request.Location.YPosition,
                LocationDescription = request.Location.LocationDescription
            } : null;
            asset.Manufacturer = request.Manufacturer;
            asset.Model = request.Model;
            asset.SerialNumber = request.SerialNumber;
            asset.InstallationDate = request.InstallationDate;
            asset.Status = request.Status;
            asset.CriticalityLevel = request.CriticalityLevel;
            asset.HealthStatus = request.HealthStatus;
            asset.ParentAssetId = request.ParentAssetId;

            assetRepo.Update(asset);
            await assetRepo.SaveChangesAsync(cancellationToken);
            return asset.Id;
        }

        // ══════════════════════════════════════════════════════════════════
        // ASSET - DELETE (via l'agrégat Site)
        // ══════════════════════════════════════════════════════════════════

        public async Task DeleteAssetAsync(Guid siteId, Guid assetId, CancellationToken cancellationToken)
        {
            var asset = await assetRepo.GetByIdAsync(assetId, cancellationToken);
            if (asset is null)
                throw new NotFoundException(nameof(Asset), assetId);

            // 2. Trouver l'équipement
            var site = await _siteRepository.AnyAsync(a => a.Id == siteId);
            if (!site)
                throw new NotFoundException(nameof(Asset), assetId);

            // 3. Valider la suppression
            ValidateDeleteAsset(asset);

            // 4. Supprimer de l'agrégat
            assetRepo.Remove(asset);

            // 5. Sauvegarder
            await assetRepo.SaveChangesAsync(cancellationToken);
        }

        // ══════════════════════════════════════════════════════════════════
        // VALIDATION MÉTIER - ASSET
        // ══════════════════════════════════════════════════════════════════

        /// <summary>
        /// Valide les clés étrangères externes à l'agrégat Site
        /// </summary>
        private async Task ValidateAssetForeignKeysAsync(Guid categoryId, Guid siteId, Guid? unitId, CancellationToken cancellationToken)
        {
            var errors = new Dictionary<string, string[]>();

            var categoryExists = await repoAssetCategory.AnyAsync(c => c.Id == categoryId, cancellationToken);
            if (!categoryExists)
            {
                errors.Add("CategoryId", ["La catégorie d'équipement spécifiée n'existe pas"]);
            }

            var site = await _siteRepository.AnyAsync(s => s.Id == siteId, cancellationToken);
            if (!site)
            {
                errors.Add("SiteId", ["Le site spécifié n'existe pas"]);
            }

            if (unitId.HasValue)
            {
                var unitExists = await repoUnit.AnyAsync(u => u.Id == unitId.Value && u.SiteId == siteId, cancellationToken);
                if (!unitExists)
                {
                    errors.Add("UnitId", ["Le lot spécifié n'existe pas ou n'appartient pas au site"]);
                }
            }

            if (errors.Count > 0)
                throw new AppValidationException(errors);
        }

        /// <summary>
        /// Validation pour l'ajout d'un équipement
        /// </summary>
        private async Task ValidateAddAsset(CreateAssetCommand request)
        {
            var errors = new Dictionary<string, string[]>();

            // Unicité de la référence dans le site
            if (await assetRepo.AnyAsync(a => a.Reference == request.Reference && a.SiteId == request.SiteId))
            {
                errors.Add(nameof(request.Reference), ["Cette référence d'équipement existe déjà pour ce site"]);
            }

            // Unicité du numéro de série (global dans le site si fourni)
            if (!string.IsNullOrEmpty(request.SerialNumber))
            {
                if (await assetRepo.AnyAsync(a => a.SerialNumber == request.SerialNumber && a.SiteId == request.SiteId))
                {
                    errors.Add(nameof(request.SerialNumber), ["Ce numéro de série existe déjà pour ce site"]);
                }
            }

            // Vérifier que le lot appartient bien au site
            if (request.UnitId.HasValue)
            {
                if (!await repoUnit.AnyAsync(u => u.Id == request.UnitId.Value && u.SiteId == request.SiteId))
                {
                    errors.Add(nameof(request.UnitId), ["Le lot spécifié n'appartient pas à ce site"]);
                }
            }

            // Vérifier que l'équipement parent appartient bien au site
            if (request.ParentAssetId.HasValue)
            {
                var parentAsset = await assetRepo.AnyAsync(a => a.Id == request.ParentAssetId.Value && a.SiteId == request.SiteId);
                if (!parentAsset)
                {
                    errors.Add(nameof(request.ParentAssetId), ["L'équipement parent spécifié n'appartient pas à ce site"]);
                }
            }

            if (errors.Count > 0)
                throw new AppValidationException(errors);
        }

        /// <summary>
        /// Validation pour la mise à jour d'un équipement
        /// </summary>
        private async Task ValidateUpdateAsset(UpdateAssetCommand request)
        {
            var errors = new Dictionary<string, string[]>();

            // Unicité de la référence (exclure l'équipement actuel)
            if (await assetRepo.AnyAsync(a => a.Reference == request.Reference && a.Id != request.Id && a.SiteId == request.SiteId))
            {
                errors.Add(nameof(request.Reference), ["Cette référence d'équipement existe déjà pour ce site"]);
            }

            // Unicité du numéro de série (exclure l'équipement actuel)
            if (!string.IsNullOrEmpty(request.SerialNumber))
            {
                if (await assetRepo.AnyAsync(a => a.SerialNumber == request.SerialNumber && a.Id != request.Id && a.SiteId == request.SiteId))
                {
                    errors.Add(nameof(request.SerialNumber), ["Ce numéro de série existe déjà pour ce site"]);
                }
            }

            // Vérifier que le lot appartient bien au site
            if (request.UnitId.HasValue)
            {
                if (!await repoUnit.AnyAsync(u => u.Id == request.UnitId.Value && u.SiteId == request.SiteId))
                {
                    errors.Add(nameof(request.UnitId), ["Le lot spécifié n'appartient pas à ce site"]);
                }
            }

            // Vérifier que l'équipement parent appartient bien au site
            if (request.ParentAssetId.HasValue)
            {
                // Empêcher la référence circulaire
                if (request.ParentAssetId.Value == request.Id)
                {
                    errors.Add(nameof(request.ParentAssetId), ["Un équipement ne peut pas être son propre parent"]);
                }
                else
                {
                    var parentAsset = await assetRepo.AnyAsync(a => a.Id == request.ParentAssetId.Value && a.SiteId == request.SiteId);
                    if (!parentAsset)
                    {
                        errors.Add(nameof(request.ParentAssetId), ["L'équipement parent spécifié n'appartient pas à ce site"]);
                    }
                    else
                    {
                        //Pas utilisé pour l'instant : TOSEE
                        // Vérifier qu'on ne crée pas de cycle (le parent n'est pas un descendant)
                        //if (IsDescendantOf(existingAsset, request.ParentAssetId.Value, site.Assets))
                        //{
                        //    errors.Add(nameof(request.ParentAssetId), ["Référence circulaire détectée : l'équipement parent est un descendant de cet équipement"]);
                        //}
                    }
                }
            }

            // Pas utilisé pour l'instant : TOSEE
            // Vérifier qu'on ne peut pas décommissionner si des sous-équipements sont actifs
            //if (request.Status == AssetStatus.Decommissioned)
            //{
            //    var hasActiveChildren = existingAsset.ChildAssets?.Any(c => c.Status == AssetStatus.Active) ?? false;
            //    if (hasActiveChildren)
            //    {
            //        errors.Add(nameof(request.Status), ["Impossible de décommissionner : des sous-équipements sont encore actifs"]);
            //    }
            //}

            if (errors.Count > 0)
                throw new AppValidationException(errors);
        }

        /// <summary>
        /// Validation pour la suppression d'un équipement
        /// </summary>
        private async Task ValidateDeleteAsset(Asset asset)
        {
            var errors = new Dictionary<string, string[]>();

            // Impossible de supprimer si sous-équipements existent
            var hasChildren = await assetRepo.AnyAsync(a => a.ParentAssetId == asset.Id && a.SiteId == asset.SiteId);
            if (hasChildren)
            {
                errors.Add("Asset", [$"Impossible de supprimer : l'équipement contient des sous-équipement(s) associé(s)"]);
            }

            // Pas utilisé pour l'instant : TOSEE
            // Impossible de supprimer si plans de maintenance actifs
            //var hasActivePlans = asset.MaintenancePlans?.Any(p => p.IsActive) ?? false;
            //if (hasActivePlans)
            //{
            //    errors.Add("Asset", ["Impossible de supprimer : des plans de maintenance actifs sont associés"]);
            //}

            // Pas utilisé pour l'instant : TOSEE
            // Impossible de supprimer si garanties actives
            //var hasActiveWarranties = asset.Warranties?.Any(w => w.IsActive && w.EndDate > DateTime.UtcNow) ?? false;
            //if (hasActiveWarranties)
            //{
            //    errors.Add("Asset", ["Impossible de supprimer : des garanties actives sont associées"]);
            //}

            if (errors.Count > 0)
                throw new AppValidationException(errors);
        }

        /// <summary>
        /// Vérifie si targetAssetId est un descendant de l'asset (pour détecter les cycles)
        /// Pas utilisé actuellement
        /// </summary>
        //private bool IsDescendantOf(Asset asset, Guid targetAssetId, ICollection<Asset> allAssets)
        //{
        //    if (asset.ChildAssets == null || !asset.ChildAssets.Any())
        //        return false;

        //    foreach (var child in asset.ChildAssets)
        //    {
        //        if (child.Id == targetAssetId)
        //            return true;

        //        if (IsDescendantOf(child, targetAssetId, allAssets))
        //            return true;
        //    }

        //    return false;
        //}

        #endregion


        #region Occupant Management
        // ══════════════════════════════════════════════════════════════════════════
        // OCCUPANT - CREATE
        // ══════════════════════════════════════════════════════════════════════════

        public async Task<Guid> AddOccupantToUnitAsync(CreateOccupantCommand request, CancellationToken cancellationToken)
        {
            var unit = await repoUnit.FirstOrDefaultAsync(u => u.Id == request.UnitId && u.SiteId == request.SiteId, cancellationToken);
            if (unit is null)
                throw new NotFoundException(nameof(Site), request.SiteId);

            // 3. Valider via l'agrégat
            await ValidateAddOccupant(request);

            // 4. Créer l'occupant
            var occupant = mapper.Map<Occupant>(request);
            // 5. Ajouter à l'agrégat
            await occupantRepo.AddAsync(occupant, cancellationToken);

            // 6. Mettre à jour le statut du lot si nécessaire
            if (unit.Status == UnitStatus.Vacant && request.MoveInDate <= DateTime.UtcNow)
            {
                unit.Status = UnitStatus.Occupied;
                repoUnit.Update(unit);
            }

            // 7. Sauvegarder
            await occupantRepo.SaveChangesAsync(cancellationToken);

            return occupant.Id;
        }

        // ══════════════════════════════════════════════════════════════════════════
        // OCCUPANT - UPDATE
        // ══════════════════════════════════════════════════════════════════════════

        public async Task<Guid> UpdateOccupantAsync(UpdateOccupantCommand request, CancellationToken cancellationToken)
        {
            var unit = await repoUnit.FirstOrDefaultAsync(u => u.Id == request.UnitId);
            if (unit == null)
                throw new NotFoundException(nameof(Unit), request.UnitId);

            var occupant = await occupantRepo.FirstOrDefaultAsync(o => o.Id == request.Id);
            if (occupant == null)
                throw new NotFoundException(nameof(Occupant), request.Id);

            await ValidateUpdateOccupant(unit, occupant, request);

            // Mise à jour
            occupant.Type = request.Type;
            occupant.PersonType = request.PersonType;
            occupant.FirstName = request.FirstName;
            occupant.LastName = request.LastName;
            occupant.CompanyName = request.CompanyName;
            occupant.Email = request.Email;
            occupant.Phone = request.Phone;
            occupant.Mobile = request.Mobile;
            occupant.PreferredContactMethod = request.PreferredContactMethod;
            occupant.MoveInDate = request.MoveInDate;
            occupant.MoveOutDate = request.MoveOutDate;
            occupant.HasPortalAccess = request.HasPortalAccess;
            occupantRepo.Update(occupant);
            await occupantRepo.SaveChangesAsync(cancellationToken);

            return occupant.Id;
        }

        // ══════════════════════════════════════════════════════════════════════════
        // OCCUPANT - DELETE
        // ══════════════════════════════════════════════════════════════════════════

        public async Task DeleteOccupantAsync(Guid siteId, Guid unitId, Guid occupantId, CancellationToken cancellationToken)
        {
            var site = await _siteRepository.AnyAsync(s => s.Id == siteId, cancellationToken);
            if (!site)
                throw new NotFoundException(nameof(Site), siteId);

            var unit = await repoUnit.FirstOrDefaultAsync(u => u.Id == unitId && u.SiteId == siteId);
            if (unit == null)
                throw new NotFoundException(nameof(Unit), unitId);

            var occupant = await occupantRepo.FirstOrDefaultAsync(o => o.Id == occupantId && o.UnitId == unitId);
            if (occupant == null)
                throw new NotFoundException(nameof(Occupant), occupantId);

            repoOccupant.Remove(occupant);

            // Mettre à jour le statut du lot si plus d'occupants actifs
            var hasActiveOccupants = await occupantRepo.AnyAsync(o => (o.MoveOutDate == null || o.MoveOutDate > DateTime.UtcNow) && o.UnitId == unitId);
            if (!hasActiveOccupants)
            {
                unit.Status = UnitStatus.Vacant;
                repoUnit.Update(unit);
            }

            await _siteRepository.SaveChangesAsync(cancellationToken);
        }

        #endregion


        #region Site keeper management
        // ══════════════════════════════════════════════════════════════════════════
        // SITE KEEPER - CREATE
        // ══════════════════════════════════════════════════════════════════════════

        public async Task<Guid> AddSiteKeeperAsync(CreateSiteKeeperCommand request, CancellationToken cancellationToken)
        {
            var site = await _siteRepository.AnyAsync(s => s.Id == request.SiteId);
            if (!site)
                throw new NotFoundException(nameof(Site), request.SiteId);

            await ValidateAddSiteKeeper(request);

            var keeper = new SiteKeeper
            {
                Id = Guid.NewGuid(),
                SiteId = request.SiteId,
                Firstname = request.FirstName,
                Lastname = request.LastName,
                Email = request.Email,
                Phone = request.Phone,
                CellPhone = request.Mobile,
                WorkingHours = request.WorkingHours,
                Location = request.Location,
                Notes = request.Notes,
                IsMainKeeper = request.IsMainKeeper
            };

            // Si c'est le gardien principal, retirer le flag des autres
            if (request.IsMainKeeper)
            {
                var keepers = await repoSiteKeeper.FilterAsync(k => k.SiteId == request.SiteId && k.IsMainKeeper, cancellationToken);
                foreach (var existingKeeper in keepers)
                {
                    existingKeeper.IsMainKeeper = false;
                    repoSiteKeeper.Update(existingKeeper);
                }
            }

            await repoSiteKeeper.AddAsync(keeper);
            await _siteRepository.SaveChangesAsync(cancellationToken);

            return keeper.Id;
        }

        // ══════════════════════════════════════════════════════════════════════════
        // SITE KEEPER - UPDATE
        // ══════════════════════════════════════════════════════════════════════════

        public async Task<Guid> UpdateSiteKeeperAsync(UpdateSiteKeeperCommand request, CancellationToken cancellationToken)
        {
            var site = await _siteRepository.AnyAsync(s => s.Id == request.SiteId);
            if (!site)
                throw new NotFoundException(nameof(Site), request.SiteId);

            var keeper = await repoSiteKeeper.FirstOrDefaultAsync(k => k.Id == request.Id);
            if (keeper == null)
                throw new NotFoundException(nameof(SiteKeeper), request.Id);

            await ValidateUpdateSiteKeeper(keeper, request);

            // Si devient gardien principal, retirer le flag des autres
            if (request.IsMainKeeper && !keeper.IsMainKeeper)
            {
                var siteKeepers = await repoSiteKeeper.FilterAsync(k => k.SiteId == request.SiteId && k.IsMainKeeper && k.Id != request.Id, cancellationToken);
                foreach (var existingKeeper in siteKeepers)
                {
                    existingKeeper.IsMainKeeper = false;
                    repoSiteKeeper.Update(existingKeeper);
                }
            }

            keeper.Firstname = request.FirstName;
            keeper.Lastname = request.LastName;
            keeper.Email = request.Email;
            keeper.Phone = request.Phone;
            keeper.CellPhone = request.Mobile;
            keeper.WorkingHours = request.WorkingHours;
            keeper.Location = request.Location;
            keeper.Notes = request.Notes;
            keeper.IsMainKeeper = request.IsMainKeeper;

            repoSiteKeeper.Update(keeper);
            await repoSiteKeeper.SaveChangesAsync(cancellationToken);

            return keeper.Id;
        }

        // ══════════════════════════════════════════════════════════════════════════
        // SITE KEEPER - DELETE
        // ══════════════════════════════════════════════════════════════════════════

        public async Task DeleteSiteKeeperAsync(Guid siteId, Guid keeperId, CancellationToken cancellationToken)
        {
            var site = await _siteRepository.AnyAsync(s => s.Id == siteId);
            if (!site)
                throw new NotFoundException(nameof(Site), siteId);

            var keeper = await repoSiteKeeper.FirstOrDefaultAsync(k => k.Id == keeperId);
            if (keeper == null)
                throw new NotFoundException(nameof(SiteKeeper), keeperId);

            repoSiteKeeper.Remove(keeper);
            await _siteRepository.SaveChangesAsync(cancellationToken);
        }
        #endregion


        #region Site Contact Management
        // ══════════════════════════════════════════════════════════════════════════
        // SITE CONTACT - CREATE
        // ══════════════════════════════════════════════════════════════════════════

        public async Task<Guid> AddSiteContactAsync(CreateSiteContactCommand request, CancellationToken cancellationToken)
        {
            var site = await repoSiteContact.AnyAsync(x => x.SiteId == request.SiteId);
            if (!site)
                throw new NotFoundException(nameof(Site), request.SiteId);

            // Valider clé étrangère externe
            await ValidateSiteContactForeignKeysAsync(request.ContactTypeId, cancellationToken);

            await ValidateAddSiteContact(request);

            var contact = new SiteContact
            {
                Id = Guid.NewGuid(),
                SiteId = request.SiteId,
                SiteContactCategoryId = request.ContactTypeId,
                Firstname = request.FirstName,
                Lastname = request.LastName,
                Email = request.Email,
                Phone = request.Phone,
                CellPhone = request.Mobile,
                //Position = request.Position,
                Note = request.Notes,
                IsPrimary = request.IsPrimary
            };

            // Si c'est le contact principal pour ce type, retirer le flag des autres
            if (request.IsPrimary)
            {
                var contacts = await repoSiteContact.FilterAsync(c => c.SiteId == request.SiteId && c.IsPrimary, cancellationToken);
                foreach (var existingContact in contacts)
                {
                    existingContact.IsPrimary = false;
                    repoSiteContact.Update(existingContact);
                }
            }

            await repoSiteContact.AddAsync(contact, cancellationToken);
            await repoSiteContact.SaveChangesAsync(cancellationToken);

            return contact.Id;
        }

        // ══════════════════════════════════════════════════════════════════════════
        // SITE CONTACT - UPDATE
        // ══════════════════════════════════════════════════════════════════════════

        public async Task<Guid> UpdateSiteContactAsync(UpdateSiteContactCommand request, CancellationToken cancellationToken)
        {
            var site = await repoSiteContact.AnyAsync(x => x.SiteId == request.SiteId);
            if (!site)
                throw new NotFoundException(nameof(Site), request.SiteId);

            var contact = await repoSiteContact.FirstOrDefaultAsync(c => c.Id == request.Id);
            if (contact == null)
                throw new NotFoundException(nameof(SiteContact), request.Id);

            await ValidateSiteContactForeignKeysAsync(request.ContactTypeId, cancellationToken);
            await ValidateUpdateSiteContact(contact, request);

            // Si devient contact principal pour ce type, retirer le flag des autres
            if (request.IsPrimary && !contact.IsPrimary)
            {
                var contacts = await repoSiteContact.FilterAsync(c => c.SiteId == request.SiteId && c.IsPrimary && c.Id != request.Id, cancellationToken);
                foreach (var existingContact in contacts)
                {
                    existingContact.IsPrimary = false;
                    repoSiteContact.Update(existingContact);
                }
            }

            contact.SiteContactCategoryId = request.ContactTypeId;
            contact.Firstname = request.FirstName;
            contact.Lastname = request.LastName;
            contact.Email = request.Email;
            contact.Phone = request.Phone;
            contact.CellPhone = request.Mobile;
            //contact.Position = request.Position;
            contact.Note = request.Notes;
            contact.IsPrimary = request.IsPrimary;
            repoSiteContact.Update(contact);
            await repoSiteContact.SaveChangesAsync(cancellationToken);

            return contact.Id;
        }

        // ══════════════════════════════════════════════════════════════════════════
        // SITE CONTACT - DELETE
        // ══════════════════════════════════════════════════════════════════════════

        public async Task DeleteSiteContactAsync(Guid siteId, Guid contactId, CancellationToken cancellationToken)
        {
            var site = await repoSiteContact.AnyAsync(x => x.SiteId == siteId);
            if (!site)
                throw new NotFoundException(nameof(Site), siteId);

            var contact = await repoSiteContact.FirstOrDefaultAsync(c => c.Id == contactId);
            if (contact == null)
                throw new NotFoundException(nameof(SiteContact), contactId);

            repoSiteContact.Remove(contact);
            await _siteRepository.SaveChangesAsync(cancellationToken);
        }
        #endregion


        #region VALIDATIONS MÉTIER
        // ══════════════════════════════════════════════════════════════════════════
        // VALIDATIONS MÉTIER
        // ══════════════════════════════════════════════════════════════════════════

        private async Task ValidateAddOccupant(CreateOccupantCommand request)
        {
            var errors = new Dictionary<string, string[]>();

            // Vérifier les chevauchements de locataires actifs
            if (request.Type == OccupantType.Tenant)
            {
                var activeTenants = await occupantRepo.CountAsync(x => x.UnitId == request.UnitId && x.Type == OccupantType.Tenant && (x.MoveOutDate == null || x.MoveOutDate > DateTime.UtcNow));

                if (activeTenants >= 2)
                {
                    errors.Add("Type", ["Ce lot a déjà 2 locataires actifs"]);
                }
            }

            // Vérifier les doublons (même personne)
            var isDuplicate = await occupantRepo.AnyAsync(o =>
                o.FirstName.Equals(request.FirstName, StringComparison.OrdinalIgnoreCase) &&
                o.LastName.Equals(request.LastName, StringComparison.OrdinalIgnoreCase) &&
                (o.MoveOutDate == null || o.MoveOutDate > DateTime.UtcNow) && o.UnitId == request.UnitId);

            if (isDuplicate)
            {
                errors.Add("LastName", ["Cette personne est déjà enregistrée comme occupant actif de ce lot"]);
            }

            if (errors.Count > 0)
                throw new AppValidationException(errors);
        }

        private async Task ValidateUpdateOccupant(Unit unit, Occupant existingOccupant, UpdateOccupantCommand request)
        {
            var errors = new Dictionary<string, string[]>();

            // Vérifier les doublons (exclure l'occupant actuel)
            var isDuplicate = await occupantRepo.AnyAsync(o =>
                o.Id != request.Id &&
                o.FirstName.Equals(request.FirstName, StringComparison.OrdinalIgnoreCase) &&
                o.LastName.Equals(request.LastName, StringComparison.OrdinalIgnoreCase) &&
                (o.MoveOutDate == null || o.MoveOutDate > DateTime.UtcNow)
                 && o.UnitId == request.UnitId);

            if (isDuplicate)
            {
                errors.Add("LastName", ["Cette personne est déjà enregistrée comme occupant actif de ce lot"]);
            }

            if (errors.Count > 0)
                throw new AppValidationException(errors);
        }

        private async Task ValidateAddSiteKeeper(CreateSiteKeeperCommand request)
        {
            var errors = new Dictionary<string, string[]>();

            // Vérifier les doublons
            var isDuplicate = await repoSiteKeeper.AnyAsync(k =>
                k.Firstname.Equals(request.FirstName, StringComparison.OrdinalIgnoreCase) &&
                k.Lastname.Equals(request.LastName, StringComparison.OrdinalIgnoreCase));

            if (isDuplicate)
            {
                errors.Add("LastName", ["Ce gardien est déjà enregistré pour ce site"]);
            }

            //// Limite du nombre de gardiens (optionnel)
            //if (site.SiteKeepers.Count >= 5)
            //{
            //    errors.Add("SiteId", ["Un site ne peut pas avoir plus de 5 gardiens"]);
            //}

            if (errors.Count > 0)
                throw new AppValidationException(errors);
        }

        private async Task ValidateUpdateSiteKeeper(SiteKeeper existingKeeper, UpdateSiteKeeperCommand request)
        {
            var errors = new Dictionary<string, string[]>();

            var isDuplicate = await repoSiteKeeper.AnyAsync(k =>
                k.Id != request.Id &&
                k.Firstname.Equals(request.FirstName, StringComparison.OrdinalIgnoreCase) &&
                k.Lastname.Equals(request.LastName, StringComparison.OrdinalIgnoreCase) && k.SiteId == existingKeeper.SiteId);

            if (isDuplicate)
            {
                errors.Add("LastName", ["Ce gardien est déjà enregistré pour ce site"]);
            }

            if (errors.Count > 0)
                throw new AppValidationException(errors);
        }

        private async Task ValidateSiteContactForeignKeysAsync(Guid contactTypeId, CancellationToken cancellationToken)
        {
            var errors = new Dictionary<string, string[]>();

            var contactTypeExists = await repoContactType.AnyAsync(c => c.Id == contactTypeId, cancellationToken);
            if (!contactTypeExists)
            {
                errors.Add("ContactTypeId", ["Le type de contact spécifié n'existe pas"]);
            }

            if (errors.Count > 0)
                throw new AppValidationException(errors);
        }

        private async Task ValidateAddSiteContact(CreateSiteContactCommand request)
        {
            var errors = new Dictionary<string, string[]>();

            // Vérifier les doublons
            var isDuplicate = await repoSiteContact.AnyAsync(c =>
                c.Firstname.Equals(request.FirstName, StringComparison.OrdinalIgnoreCase) &&
                c.Lastname.Equals(request.LastName, StringComparison.OrdinalIgnoreCase) &&
                c.SiteContactCategoryId == request.ContactTypeId);

            if (isDuplicate)
            {
                errors.Add("LastName", ["Ce contact existe déjà avec le même type pour ce site"]);
            }

            if (errors.Count > 0)
                throw new AppValidationException(errors);
        }

        private async Task ValidateUpdateSiteContact(SiteContact existingContact, UpdateSiteContactCommand request)
        {
            var errors = new Dictionary<string, string[]>();

            var isDuplicate = await repoSiteContact.AnyAsync(c =>
                c.Id != request.Id &&
                c.Firstname.Equals(request.FirstName, StringComparison.OrdinalIgnoreCase) &&
                c.Lastname.Equals(request.LastName, StringComparison.OrdinalIgnoreCase) &&
                c.SiteContactCategoryId == request.ContactTypeId
                && c.SiteId == existingContact.SiteId);

            if (isDuplicate)
            {
                errors.Add("LastName", ["Ce contact existe déjà avec le même type pour ce site"]);
            }

            if (errors.Count > 0)
                throw new AppValidationException(errors);
        }

        #endregion
    }
}
