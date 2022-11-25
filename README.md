# First_Task
## Day 1(03.11.2022)
   * IdentityController
   * Models -> Entities -> Employee.cs, Department.cs, Permission.cs, Position.cs, User.cs
   * AppDbContext.cs
   * Services -> Departments -> IDepartmentService

## Day 2(04.11.2022)
   * BaseController
   * Models -> Shared -> BaseEntity.cs, CommonEntity.cs
   * Models -> Dtos -> Departments
   * Services -> Departments -> DepartmentManager
   * PositionController

## Day 3(07.11.2022)
   * DepartmentController
   * Models -> Dtos -> Positions
   * Services -> Positions -> IPositionService.cs, PositionManager.cs
   * Models -> Shared
   * Models -> Dtos -> Positions
   * Models -> Entities -> EmployeeDepartment.cs
   * Repositories -> IBaseRepository.cs, BaseRespository.cs
   * Routes

## Day 4(08.11.2022)
   * Services -> Users -> IUserService.cs, UserManager.cs
   * Models -> Dtos -> Users
   * IdentityController -> Login, Register
   * JwtSettings.cs
   * Mappings

## Day 5(09.11.2022)
   * Services -> Tokens -> ITokenService.cs, TokenManager.cs
   * Models -> Dtos -> Tokens, BaseDto.cs
   * ManagingController

## Day 6(12.11.2022)
   * UserPermissionController
   * Middlewares -> JwtMiddleware
   * New Migration
   * Models -> Entities -> UserPermission.cs

## Day 7(15.11.2022)
   * Models -> Dtos -> UserPermission
   * Routes -> ApiRoutes -> UserPermission, Manage
   * Services -> Manages -> IManageService.cs

## Day 8(17.11.2022)
   * Services -> Manages -> ManageManager.cs
   * Services -> UserPermissions -> IUserPermissionService.cs
   * ExceptionMiddleware
   
## Day 9(20.11.2022)
   * Validators -> DepartmentValidator, PermissionValidator, UserValidator, EmployeeValidator, PositionValidator
   * New Migration
   * Services -> UserPermissions -> IUserPermissionManager.cs

## Day 10(23.11.2022)
   * UnitOfWork -> IUnitOfWorkService, IUnitOfWorkManager
   * New Migration
   * PermissionSet -> PermissionNames
   * Extensions -> ClaimRequirementFilter
   
## Day 11(25.11.2022)
   * Check