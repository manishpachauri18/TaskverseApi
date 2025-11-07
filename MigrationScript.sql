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
    [Id] nvarchar(450) NOT NULL,
    [Name] nvarchar(256) NULL,
    [NormalizedName] nvarchar(256) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AspNetUsers] (
    [Id] nvarchar(450) NOT NULL,
    [FullName] nvarchar(max) NOT NULL,
    [ProfilePicturePath] nvarchar(max) NULL,
    [RoleName] nvarchar(max) NULL,
    [CreatedAt] datetime2 NOT NULL,
    [LastLoginAt] datetime2 NULL,
    [IsActive] bit NOT NULL,
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

CREATE TABLE [AspNetRoleClaims] (
    [Id] int NOT NULL IDENTITY,
    [RoleId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserClaims] (
    [Id] int NOT NULL IDENTITY,
    [UserId] nvarchar(450) NOT NULL,
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
    [UserId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
    CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserRoles] (
    [UserId] nvarchar(450) NOT NULL,
    [RoleId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
    CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserTokens] (
    [UserId] nvarchar(450) NOT NULL,
    [LoginProvider] nvarchar(450) NOT NULL,
    [Name] nvarchar(450) NOT NULL,
    [Value] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
    CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Projects] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NULL,
    [CreatedById] nvarchar(450) NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [ModifiedAt] datetime2 NULL,
    [ModifiedById] nvarchar(max) NULL,
    [IsDeleted] bit NOT NULL,
    [DeletedAt] datetime2 NULL,
    [RowVersion] rowversion NULL,
    CONSTRAINT [PK_Projects] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Projects_AspNetUsers_CreatedById] FOREIGN KEY ([CreatedById]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [RefreshTokens] (
    [Id] int NOT NULL IDENTITY,
    [Token] nvarchar(450) NOT NULL,
    [UserId] nvarchar(450) NOT NULL,
    [DeviceInfo] nvarchar(max) NOT NULL,
    [IpAddress] nvarchar(max) NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [ExpiresAt] datetime2 NOT NULL,
    [RevokedAt] datetime2 NULL,
    [ReplacedByToken] nvarchar(max) NULL,
    CONSTRAINT [PK_RefreshTokens] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_RefreshTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [ProjectUsers] (
    [ProjectId] int NOT NULL,
    [UserId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_ProjectUsers] PRIMARY KEY ([ProjectId], [UserId]),
    CONSTRAINT [FK_ProjectUsers_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_ProjectUsers_Projects_ProjectId] FOREIGN KEY ([ProjectId]) REFERENCES [Projects] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [TaskItems] (
    [Id] int NOT NULL IDENTITY,
    [Title] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NULL,
    [ProjectId] int NOT NULL,
    [AssignedToId] nvarchar(450) NULL,
    [CreatedAt] datetime2 NOT NULL,
    [ModifiedAt] datetime2 NULL,
    [ModifiedById] nvarchar(max) NULL,
    [DueDate] datetime2 NULL,
    [Status] nvarchar(max) NOT NULL,
    [IsDeleted] bit NOT NULL,
    [DeletedAt] datetime2 NULL,
    [RowVersion] rowversion NULL,
    CONSTRAINT [PK_TaskItems] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_TaskItems_AspNetUsers_AssignedToId] FOREIGN KEY ([AssignedToId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE SET NULL,
    CONSTRAINT [FK_TaskItems_Projects_ProjectId] FOREIGN KEY ([ProjectId]) REFERENCES [Projects] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Comments] (
    [Id] int NOT NULL IDENTITY,
    [Content] nvarchar(max) NOT NULL,
    [TaskItemId] int NOT NULL,
    [CreatedById] nvarchar(450) NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [IsDeleted] bit NOT NULL,
    [DeletedAt] datetime2 NULL,
    CONSTRAINT [PK_Comments] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Comments_AspNetUsers_CreatedById] FOREIGN KEY ([CreatedById]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Comments_TaskItems_TaskItemId] FOREIGN KEY ([TaskItemId]) REFERENCES [TaskItems] ([Id]) ON DELETE NO ACTION
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

CREATE INDEX [IX_Comments_CreatedById] ON [Comments] ([CreatedById]);
GO

CREATE INDEX [IX_Comments_TaskItemId] ON [Comments] ([TaskItemId]);
GO

CREATE INDEX [IX_Projects_CreatedById] ON [Projects] ([CreatedById]);
GO

CREATE INDEX [IX_ProjectUsers_UserId] ON [ProjectUsers] ([UserId]);
GO

CREATE INDEX [IX_RefreshTokens_Token] ON [RefreshTokens] ([Token]);
GO

CREATE INDEX [IX_RefreshTokens_UserId] ON [RefreshTokens] ([UserId]);
GO

CREATE INDEX [IX_TaskItems_AssignedToId] ON [TaskItems] ([AssignedToId]);
GO

CREATE INDEX [IX_TaskItems_ProjectId] ON [TaskItems] ([ProjectId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251013162441_InitialCreate', N'8.0.20');
GO

COMMIT;
GO

