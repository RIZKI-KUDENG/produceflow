using ProduceFlow.Application.Interfaces;
using ProduceFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using ProduceFlow.Infrastructure.Services;

namespace ProduceFlow.Infrastructure.Data;

public static class DbInitializer
{
    public static async Task InitializeAsync(AppDbContext context, IPasswordHasher PasswordHasher)
    {
        if (await context.Assets.AnyAsync())
        {
            return;   
        }
        // Seed Roles
        var adminRole = new Role { Name = "Admin", Description = "Full access"};
        var staffRole = new Role { Name = "User", Description = "Request access"};
        var managerRole = new Role { Name = "Manager", Description = "Approval access"};

        var role = new List<Role> { adminRole, staffRole, managerRole };
        context.Roles.AddRange(role);
        // Seed Departments
        var itDept = new Department { Name = "IT", CostCenterCode = "IT001" };
        var hrDept = new Department { Name = "Human Resources", CostCenterCode = "HR001" };
        var opsDept = new Department { Name = "Operations", CostCenterCode = "OPS001" };
        var departments = new List<Department> { itDept, hrDept, opsDept };
        context.Departments.AddRange(departments);

        // Seed Locations
        var hqServerRoom = new Location { Name = "HQ-Server Room", Address = "Building A, Floor 2" };
        var hqOffice = new Location { Name = "HQ-Office", Address = "Building A, Floor 5" };
        var werehouse = new Location { Name = "Warehouse Sukabumi", Address = "Jl. Raya Sukabumi" };

        var locs = new List<Location> { hqServerRoom, hqOffice, werehouse };
        context.Locations.AddRange(locs);

        // Seed Categories
        var catLaptop = new Category { Name = "Laptop", DepreciationYears = 3 };
        var catFurniture = new Category { Name = "Furniture", DepreciationYears = 5 };
        var catVehicle = new Category { Name = "Vehicle", DepreciationYears = 7 };
        var categories = new List<Category> { catLaptop, catFurniture, catVehicle };
        context.Categories.AddRange(categories);

        // Seed Vendor
        var vendor1 = new Vendor { Name = "PT. Tech Supplies", Address = "Jl. Merdeka No. 123, Jakarta", ContactPerson = "John Doe", Phone = "085877324567", IsVerified = true };
        var vendor2 = new Vendor { Name = "PT. Furniture Factory", Address = "Jl. Jend. Sudirman No. 456, Jakarta", ContactPerson = "Jane Smith", Phone = "085622567789", IsVerified = true };
        var vendors = new List<Vendor> { vendor1, vendor2 };
        context.Vendors.AddRange(vendors);

        await context.SaveChangesAsync();

        // Seed user 
        var adminUser = new User
        {
            FullName = "Administrator",
            Email = "admin@produceflow.com",
            PasswordHash = PasswordHasher.Hash("Admin123!"),
            DepartmentId = itDept.Id,
            IsActive = true
        };
        var staffUser = new User
        {
            FullName = "M. Rizki Arya Nanda",
            Email = "rizki0611@gmail.com",
            PasswordHash = PasswordHasher.Hash("Denk101ad222@"),
            DepartmentId = opsDept.Id,
            IsActive = true  
        };
        var managerUser = new User
        {
            FullName = "Budi Managaer",
            Email = "manager@produceflow.com",
            PasswordHash = PasswordHasher.Hash("Manager123!"),
            DepartmentId = hrDept.Id,
            IsActive = true
        };
        context.Users.AddRange(adminUser, staffUser, managerUser);
        await context.SaveChangesAsync();
        // Assign Roles to Users
        context.UserRoles.Add(new UserRole { UserId = adminUser.Id, RoleId = adminRole.Id});
        context.UserRoles.Add(new UserRole { UserId = staffUser.Id, RoleId = staffRole.Id});
        context.UserRoles.Add(new UserRole { UserId = managerUser.Id, RoleId = managerRole.Id});
        await context.SaveChangesAsync();

        // Seed Assets
        var asset1 = new Asset
        {
            Name = "Macbook Air M1",
            CategoryId = catLaptop.Id,
            LocationId = hqOffice.Id,
            CurrentHolderId = staffUser.Id,
            PurchasePrice = 15000000,
            PurchaseDate = DateTime.UtcNow.AddMonths(-2),
            Status = "In Use",
            SerialNumber = "MBAM1-20220115-001"
        };
        var asset2 = new Asset
        {
            Name = "Dell XPS 15",
            SerialNumber = "SN-DELL-002",
            CategoryId = catLaptop.Id,
            LocationId = hqServerRoom.Id,
            CurrentHolderId = null, // Available
            PurchaseDate = DateTime.UtcNow.AddMonths(-6),
            PurchasePrice = 18000000,
            Status = "Available"
        };

        var asset3 = new Asset
        {
            Name = "Kursi Ergonomis",
            SerialNumber = "FUR-001",
            CategoryId = catFurniture.Id,
            LocationId = hqOffice.Id,
            CurrentHolderId = adminUser.Id,
            PurchaseDate = DateTime.UtcNow.AddYears(-1),
            PurchasePrice = 2500000,
            Status = "In Use"
        };
        
         var asset4 = new Asset
        {
            Name = "Toyota Avanza",
            SerialNumber = "D 1234 ABC",
            CategoryId = catVehicle.Id,
            LocationId = werehouse.Id,
            CurrentHolderId = null,
            PurchaseDate = DateTime.UtcNow.AddYears(-2),
            PurchasePrice = 200000000,
            Status = "Maintenance" 
        };
        context.Assets.AddRange(asset1, asset2, asset3, asset4);

        // seed Purchase Requests
        var pr1 = new PurchaseRequest
        {
            RequestNumber = "PR-2026-001",
            RequesterId = staffUser.Id,
            RequestDate = DateTime.UtcNow.AddDays(-5),
            Reason = "Laptop lama sudah lambat, butuh upgrade untuk development.",
            Status = "Pending",
            TotalEstimatedCost = 30000000
        };
        context.PurchaseRequests.Add(pr1);
        await context.SaveChangesAsync();

        var prItem1 = new PurchaseRequestItem
        {
            PurchaseRequestId = pr1.Id,
            ItemName = "Macbook Pro M2",
            Specifications = "Apple M2, 16GB RAM, 512GB SSD",
            Quantity = 2,
            EstimatedCost = 20000000,
            VendorId = vendor1.Id
        };
        context.PurchaseRequestItems.Add(prItem1);
        await context.SaveChangesAsync();

    }
}