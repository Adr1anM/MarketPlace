IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [AspNetRoles] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(256) NULL,
    [NormalizedName] nvarchar(256) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AspNetUsers] (
    [Id] int NOT NULL IDENTITY,
    [UserName] nvarchar(256) NULL,
    [NormalizedUserName] nvarchar(256) NULL,
    [Email] nvarchar(256) NULL,
    [NormalizedEmail] nvarchar(256) NULL,
    [EmailConfirmed] bit NOT NULL,
    [PasswordHash] nvarchar(max) NULL,
    [SecurityStamp] nvarchar(max) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [PhoneNumberConfirmed] bit NOT NULL,
    [TwoFactorEnabled] bit NOT NULL,
    [LockoutEnd] datetimeoffset NULL,
    [LockoutEnabled] bit NOT NULL,
    [AccessFailedCount] int NOT NULL,
    CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Authors] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Surname] nvarchar(max) NOT NULL,
    [EmailAdress] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Authors] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Categories] (
    [Id] int NOT NULL IDENTITY,
    [CategorieName] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Categories] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Promocodes] (
    [Id] int NOT NULL IDENTITY,
    [Code] nvarchar(max) NOT NULL,
    [CreateDate] datetime2 NOT NULL,
    [ExpireDate] datetime2 NOT NULL,
    CONSTRAINT [PK_Promocodes] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AspNetRoleClaims] (
    [Id] int NOT NULL IDENTITY,
    [RoleId] int NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserClaims] (
    [Id] int NOT NULL IDENTITY,
    [UserId] int NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserLogins] (
    [LoginProvider] nvarchar(450) NOT NULL,
    [ProviderKey] nvarchar(450) NOT NULL,
    [ProviderDisplayName] nvarchar(max) NULL,
    [UserId] int NOT NULL,
    CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
    CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserRoles] (
    [UserId] int NOT NULL,
    [RoleId] int NOT NULL,
    CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
    CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserTokens] (
    [UserId] int NOT NULL,
    [LoginProvider] nvarchar(450) NOT NULL,
    [Name] nvarchar(450) NOT NULL,
    [Value] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
    CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Products] (
    [Id] int NOT NULL IDENTITY,
    [Title] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [CategorieID] int NOT NULL,
    CONSTRAINT [PK_Products] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Products_Categories_CategorieID] FOREIGN KEY ([CategorieID]) REFERENCES [Categories] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [SubCategories] (
    [Id] int NOT NULL IDENTITY,
    [SubCategoryName] nvarchar(max) NOT NULL,
    [CategoryId] int NULL,
    CONSTRAINT [PK_SubCategories] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_SubCategories_Categories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Categories] ([Id])
);
GO

CREATE TABLE [Orders] (
    [Id] int NOT NULL IDENTITY,
    [UserId] int NOT NULL,
    [OrderDate] datetime2 NOT NULL,
    [PromocodeId] int NOT NULL,
    [Quantity] int NOT NULL,
    [ShippingAdress] nvarchar(max) NOT NULL,
    [Status] int NOT NULL,
    CONSTRAINT [PK_Orders] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Orders_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Orders_Promocodes_PromocodeId] FOREIGN KEY ([PromocodeId]) REFERENCES [Promocodes] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Posts] (
    [Id] int NOT NULL IDENTITY,
    [ProductId] int NOT NULL,
    [Price] decimal(18,2) NOT NULL,
    [Created] datetime2 NOT NULL,
    CONSTRAINT [PK_Posts] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Posts_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [PostOrder] (
    [Id] int NOT NULL IDENTITY,
    [PostId] int NOT NULL,
    [OrderId] int NOT NULL,
    CONSTRAINT [PK_PostOrder] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_PostOrder_Orders_OrderId] FOREIGN KEY ([OrderId]) REFERENCES [Orders] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_PostOrder_Posts_PostId] FOREIGN KEY ([PostId]) REFERENCES [Posts] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
GO

CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;
GO

CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
GO

CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
GO

CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
GO

CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
GO

CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;
GO

CREATE UNIQUE INDEX [IX_Orders_PromocodeId] ON [Orders] ([PromocodeId]);
GO

CREATE INDEX [IX_Orders_UserId] ON [Orders] ([UserId]);
GO

CREATE INDEX [IX_PostOrder_OrderId] ON [PostOrder] ([OrderId]);
GO

CREATE INDEX [IX_PostOrder_PostId] ON [PostOrder] ([PostId]);
GO

CREATE INDEX [IX_Posts_ProductId] ON [Posts] ([ProductId]);
GO

CREATE INDEX [IX_Products_CategorieID] ON [Products] ([CategorieID]);
GO

CREATE INDEX [IX_SubCategories_CategoryId] ON [SubCategories] ([CategoryId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240409094036_InitialCreate', N'8.0.3');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [AspNetRoleClaims] DROP CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId];
GO

ALTER TABLE [AspNetUserClaims] DROP CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId];
GO

ALTER TABLE [AspNetUserLogins] DROP CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId];
GO

ALTER TABLE [AspNetUserRoles] DROP CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId];
GO

ALTER TABLE [AspNetUserRoles] DROP CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId];
GO

ALTER TABLE [AspNetUserTokens] DROP CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId];
GO

ALTER TABLE [Orders] DROP CONSTRAINT [FK_Orders_AspNetUsers_UserId];
GO

ALTER TABLE [AspNetUserTokens] DROP CONSTRAINT [PK_AspNetUserTokens];
GO

ALTER TABLE [AspNetUsers] DROP CONSTRAINT [PK_AspNetUsers];
GO

ALTER TABLE [AspNetUserRoles] DROP CONSTRAINT [PK_AspNetUserRoles];
GO

ALTER TABLE [AspNetUserLogins] DROP CONSTRAINT [PK_AspNetUserLogins];
GO

ALTER TABLE [AspNetUserClaims] DROP CONSTRAINT [PK_AspNetUserClaims];
GO

ALTER TABLE [AspNetRoles] DROP CONSTRAINT [PK_AspNetRoles];
GO

ALTER TABLE [AspNetRoleClaims] DROP CONSTRAINT [PK_AspNetRoleClaims];
GO

IF SCHEMA_ID(N'Auth') IS NULL EXEC(N'CREATE SCHEMA [Auth];');
GO

EXEC sp_rename N'[AspNetUserTokens]', N'UserRoles';
ALTER SCHEMA [Auth] TRANSFER [UserRoles];
GO

EXEC sp_rename N'[AspNetUsers]', N'Users';
ALTER SCHEMA [Auth] TRANSFER [Users];
GO

EXEC sp_rename N'[AspNetUserRoles]', N'UserRole';
ALTER SCHEMA [Auth] TRANSFER [UserRole];
GO

EXEC sp_rename N'[AspNetUserLogins]', N'UserLogins';
ALTER SCHEMA [Auth] TRANSFER [UserLogins];
GO

EXEC sp_rename N'[AspNetUserClaims]', N'UserClaims';
ALTER SCHEMA [Auth] TRANSFER [UserClaims];
GO

EXEC sp_rename N'[AspNetRoles]', N'Roles';
ALTER SCHEMA [Auth] TRANSFER [Roles];
GO

EXEC sp_rename N'[AspNetRoleClaims]', N'RoleClaims';
ALTER SCHEMA [Auth] TRANSFER [RoleClaims];
GO

EXEC sp_rename N'[Auth].[UserRole].[IX_AspNetUserRoles_RoleId]', N'IX_UserRole_RoleId', N'INDEX';
GO

EXEC sp_rename N'[Auth].[UserLogins].[IX_AspNetUserLogins_UserId]', N'IX_UserLogins_UserId', N'INDEX';
GO

EXEC sp_rename N'[Auth].[UserClaims].[IX_AspNetUserClaims_UserId]', N'IX_UserClaims_UserId', N'INDEX';
GO

EXEC sp_rename N'[Auth].[RoleClaims].[IX_AspNetRoleClaims_RoleId]', N'IX_RoleClaims_RoleId', N'INDEX';
GO

ALTER TABLE [Auth].[UserRoles] ADD CONSTRAINT [PK_UserRoles] PRIMARY KEY ([UserId], [LoginProvider], [Name]);
GO

ALTER TABLE [Auth].[Users] ADD CONSTRAINT [PK_Users] PRIMARY KEY ([Id]);
GO

ALTER TABLE [Auth].[UserRole] ADD CONSTRAINT [PK_UserRole] PRIMARY KEY ([UserId], [RoleId]);
GO

ALTER TABLE [Auth].[UserLogins] ADD CONSTRAINT [PK_UserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]);
GO

ALTER TABLE [Auth].[UserClaims] ADD CONSTRAINT [PK_UserClaims] PRIMARY KEY ([Id]);
GO

ALTER TABLE [Auth].[Roles] ADD CONSTRAINT [PK_Roles] PRIMARY KEY ([Id]);
GO

ALTER TABLE [Auth].[RoleClaims] ADD CONSTRAINT [PK_RoleClaims] PRIMARY KEY ([Id]);
GO

ALTER TABLE [Orders] ADD CONSTRAINT [FK_Orders_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Auth].[Users] ([Id]) ON DELETE CASCADE;
GO

ALTER TABLE [Auth].[RoleClaims] ADD CONSTRAINT [FK_RoleClaims_Roles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Auth].[Roles] ([Id]) ON DELETE CASCADE;
GO

ALTER TABLE [Auth].[UserClaims] ADD CONSTRAINT [FK_UserClaims_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Auth].[Users] ([Id]) ON DELETE CASCADE;
GO

ALTER TABLE [Auth].[UserLogins] ADD CONSTRAINT [FK_UserLogins_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Auth].[Users] ([Id]) ON DELETE CASCADE;
GO

ALTER TABLE [Auth].[UserRole] ADD CONSTRAINT [FK_UserRole_Roles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Auth].[Roles] ([Id]) ON DELETE CASCADE;
GO

ALTER TABLE [Auth].[UserRole] ADD CONSTRAINT [FK_UserRole_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Auth].[Users] ([Id]) ON DELETE CASCADE;
GO

ALTER TABLE [Auth].[UserRoles] ADD CONSTRAINT [FK_UserRoles_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Auth].[Users] ([Id]) ON DELETE CASCADE;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240409095150_CustomIdentityNames', N'8.0.3');
GO

COMMIT;
GO

