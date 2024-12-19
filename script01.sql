﻿create schema if not exists auth;
create schema if not exists core;
create schema if not exists "order";

create table if not exists auth."ApiResources"
(
    "Id"          integer generated by default as identity
    constraint "PK_ApiResources"
    primary key,
    "Name"        text not null,
    "DisplayName" text not null
);

alter table auth."ApiResources"
    owner to myuser;

create table if not exists auth."ApiResourceScopes"
(
    "Id"            integer generated by default as identity
    constraint "PK_ApiResourceScopes"
    primary key,
    "Scope"         text    not null,
    "ApiResourceId" integer not null
);

alter table auth."ApiResourceScopes"
    owner to myuser;

create table if not exists auth."AspNetRoles"
(
    "Id"               text not null
    constraint "PK_AspNetRoles"
    primary key,
    "Name"             varchar(256),
    "NormalizedName"   varchar(256),
    "ConcurrencyStamp" text
    );

alter table auth."AspNetRoles"
    owner to myuser;

create unique index if not exists "RoleNameIndex"
    on auth."AspNetRoles" ("NormalizedName");

create table if not exists auth."AspNetUsers"
(
    "Id"                   text    not null
    constraint "PK_AspNetUsers"
    primary key,
    "UserName"             varchar(256),
    "NormalizedUserName"   varchar(256),
    "Email"                varchar(256),
    "NormalizedEmail"      varchar(256),
    "EmailConfirmed"       boolean not null,
    "PasswordHash"         text,
    "SecurityStamp"        text,
    "ConcurrencyStamp"     text,
    "PhoneNumber"          text,
    "PhoneNumberConfirmed" boolean not null,
    "TwoFactorEnabled"     boolean not null,
    "LockoutEnd"           timestamp with time zone,
                                         "LockoutEnabled"       boolean not null,
                                         "AccessFailedCount"    integer not null
                                         );

alter table auth."AspNetUsers"
    owner to myuser;

create index if not exists "EmailIndex"
    on auth."AspNetUsers" ("NormalizedEmail");

create unique index if not exists "UserNameIndex"
    on auth."AspNetUsers" ("NormalizedUserName");

create table if not exists auth."ClientGrantTypes"
(
    "Id"        integer generated by default as identity
    constraint "PK_ClientGrantTypes"
    primary key,
    "ClientId"  integer not null,
    "GrantType" text    not null
);

alter table auth."ClientGrantTypes"
    owner to myuser;

create table if not exists auth."Clients"
(
    "Id"         integer generated by default as identity
    constraint "PK_Clients"
    primary key,
    "ClientId"   text not null,
    "ClientName" text not null
);

alter table auth."Clients"
    owner to myuser;

create table if not exists auth."ClientScopes"
(
    "Id"       integer generated by default as identity
    constraint "PK_ClientScopes"
    primary key,
    "Scope"    text    not null,
    "ClientId" integer not null
);

alter table auth."ClientScopes"
    owner to myuser;

create table if not exists auth."ClientSecrets"
(
    "Id"         integer generated by default as identity
    constraint "PK_ClientSecrets"
    primary key,
    "SecretName" text    not null,
    "ClientId"   integer not null
);

alter table auth."ClientSecrets"
    owner to myuser;

create table if not exists auth."RolePermission"
(
    "Id"         integer generated by default as identity
    constraint "PK_RolePermission"
    primary key,
    "Permission" text not null,
    "Role"       text not null
);

alter table auth."RolePermission"
    owner to myuser;

create table if not exists auth."AspNetRoleClaims"
(
    "Id"         integer generated by default as identity
    constraint "PK_AspNetRoleClaims"
    primary key,
    "RoleId"     text not null
    constraint "FK_AspNetRoleClaims_AspNetRoles_RoleId"
    references auth."AspNetRoles"
    on delete cascade,
    "ClaimType"  text,
    "ClaimValue" text
);

alter table auth."AspNetRoleClaims"
    owner to myuser;

create index if not exists "IX_AspNetRoleClaims_RoleId"
    on auth."AspNetRoleClaims" ("RoleId");

create table if not exists auth."Account"
(
    "Id"          text                 not null
    constraint "PK_Account"
    primary key
    constraint "FK_Account_AspNetUsers_Id"
    references auth."AspNetUsers"
    on delete cascade,
    "ClientId"    integer              not null,
    "CreatedDate" timestamp            not null,
    "IsActive"    boolean default true not null
);

alter table auth."Account"
    owner to myuser;

create table if not exists auth."AspNetUserClaims"
(
    "Id"         integer generated by default as identity
    constraint "PK_AspNetUserClaims"
    primary key,
    "UserId"     text not null
    constraint "FK_AspNetUserClaims_AspNetUsers_UserId"
    references auth."AspNetUsers"
    on delete cascade,
    "ClaimType"  text,
    "ClaimValue" text
);

alter table auth."AspNetUserClaims"
    owner to myuser;

create index if not exists "IX_AspNetUserClaims_UserId"
    on auth."AspNetUserClaims" ("UserId");

create table if not exists auth."AspNetUserLogins"
(
    "LoginProvider"       text not null,
    "ProviderKey"         text not null,
    "ProviderDisplayName" text,
    "UserId"              text not null
    constraint "FK_AspNetUserLogins_AspNetUsers_UserId"
    references auth."AspNetUsers"
    on delete cascade,
    constraint "PK_AspNetUserLogins"
    primary key ("LoginProvider", "ProviderKey")
    );

alter table auth."AspNetUserLogins"
    owner to myuser;

create index if not exists "IX_AspNetUserLogins_UserId"
    on auth."AspNetUserLogins" ("UserId");

create table if not exists auth."AspNetUserRoles"
(
    "UserId" text not null
    constraint "FK_AspNetUserRoles_AspNetUsers_UserId"
    references auth."AspNetUsers"
    on delete cascade,
    "RoleId" text not null
    constraint "FK_AspNetUserRoles_AspNetRoles_RoleId"
    references auth."AspNetRoles"
    on delete cascade,
    constraint "PK_AspNetUserRoles"
    primary key ("UserId", "RoleId")
    );

alter table auth."AspNetUserRoles"
    owner to myuser;

create index if not exists "IX_AspNetUserRoles_RoleId"
    on auth."AspNetUserRoles" ("RoleId");

create table if not exists auth."AspNetUserTokens"
(
    "UserId"        text not null
    constraint "FK_AspNetUserTokens_AspNetUsers_UserId"
    references auth."AspNetUsers"
    on delete cascade,
    "LoginProvider" text not null,
    "Name"          text not null,
    "Value"         text,
    constraint "PK_AspNetUserTokens"
    primary key ("UserId", "LoginProvider", "Name")
    );

alter table auth."AspNetUserTokens"
    owner to myuser;


create table if not exists core."Admin"
(
    "Id"          uuid                 not null
    constraint "PK_Admin"
    primary key,
    "DisplayName" varchar(100)         not null,
    "Email"       varchar              not null,
    "IsActive"    boolean default true not null,
    "Avatar"      text                 not null
    );

alter table core."Admin"
    owner to myuser;

create table if not exists core."Customer"
(
    "Id"          uuid                  not null
    constraint "PK_Customer"
    primary key,
    "DisplayName" varchar(100)          not null,
    "Email"       varchar               not null,
    "PhoneNumber" varchar(10)           not null,
    "IsActive"    boolean default false not null,
    "Avatar"      text                  not null,
    "BoomCount"   integer default 0     not null
    );

alter table core."Customer"
    owner to myuser;

create table if not exists core."Restaurant"
(
    "Id"          uuid                  not null
    constraint "PK_Restaurant"
    primary key,
    "DisplayName" varchar(100)          not null,
    "Email"       varchar               not null,
    "PhoneNumber" varchar(10)           not null,
    "IsActive"    boolean default false not null,
    "Avatar"      text                  not null,
    "Coordinate"  text                  not null
    );

alter table core."Restaurant"
    owner to myuser;

create table if not exists core."Shipper"
(
    "Id"          uuid         not null
    constraint "PK_Shipper"
    primary key,
    "DisplayName" varchar(100) not null,
    "Phone"       varchar(10)  not null,
    "Avatar"      text         not null,
    "Coordinate"  text         not null,
    "Status"      boolean      not null
    );

alter table core."Shipper"
    owner to myuser;

create table if not exists core."Location"
(
    "Id"         integer generated by default as identity
    constraint "PK_Location"
    primary key,
    "Name"       text    not null,
    "Phone"      varchar not null,
    "Coordinate" text    not null,
    "CustomerId" uuid    not null
    constraint "FK_Location_Customer_CustomerId"
    references core."Customer"
    on delete restrict
);

alter table core."Location"
    owner to myuser;

create index if not exists "IX_Location_CustomerId"
    on core."Location" ("CustomerId");

create table if not exists core."Calendar"
(
    "Id"           bigint generated by default as identity
    constraint "PK_Calendar"
    primary key,
    "RestaurantId" uuid                                                       not null
    constraint "FK_Calendar_Restaurant_RestaurantId"
    references core."Restaurant"
    on delete restrict,
    "EndTime"      timestamp default '-infinity'::timestamp without time zone not null,
    "StartTime"    timestamp default '-infinity'::timestamp without time zone not null
);

alter table core."Calendar"
    owner to myuser;

create index if not exists "IX_Calendar_RestaurantId"
    on core."Calendar" ("RestaurantId");

create table if not exists core."Employee"
(
    "Id"           uuid                 not null
    constraint "PK_Employee"
    primary key,
    "DisplayName"  varchar(100)         not null,
    "Email"        varchar              not null,
    "PhoneNumber"  varchar(10)          not null,
    "IsActive"     boolean default true not null,
    "Avatar"       text                 not null,
    "Role"         integer              not null,
    "RestaurantId" uuid                 not null
    constraint "FK_Employee_Restaurant_RestaurantId"
    references core."Restaurant"
    on delete restrict
    );

alter table core."Employee"
    owner to myuser;

create index if not exists "IX_Employee_RestaurantId"
    on core."Employee" ("RestaurantId");

create table if not exists core."Notification"
(
    "Id"         bigint generated by default as identity
    constraint "PK_Notification"
    primary key,
    "Title"      text not null,
    "Content"    text not null,
    "UserId"     text not null,
    "NotifyType" text not null
);

alter table core."Notification"
    owner to myuser;

create table if not exists core."SignalRConnection"
(
    "Id"           integer generated by default as identity
    constraint "PK_SignalRConnection"
    primary key,
    "ConnectionId" text not null,
    "UserId"       text not null
);

alter table core."SignalRConnection"
    owner to myuser;


create table if not exists "order"."Customer"
(
    "Id"        text                not null
    constraint "PK_Customer"
    primary key,
    "Name"      text                not null,
    "Avatar"    text                not null,
    "BoomCount" integer default 0   not null,
    "NoshPoint" numeric default 0.0 not null
);

alter table "order"."Customer"
    owner to myuser;

create table if not exists "order"."Employee"
(
    "Id"           text                  not null
    constraint "PK_Employee"
    primary key,
    "Name"         text                  not null,
    "Avatar"       text                  not null,
    "Role"         integer               not null,
    "RestaurantId" text default ''::text not null
);

alter table "order"."Employee"
    owner to myuser;

create table if not exists "order"."PaymentMethod"
(
    "Id"    uuid    not null
    constraint "PK_PaymentMethod"
    primary key,
    "Name"  varchar not null,
    "Image" text    not null
);

alter table "order"."PaymentMethod"
    owner to myuser;

create table if not exists "order"."Restaurant"
(
    "Id"         text                  not null
    constraint "PK_Restaurant"
    primary key,
    "Name"       text                  not null,
    "Coordinate" text                  not null,
    "Phone"      varchar               not null,
    "Avatar"     text                  not null,
    "IsActive"   boolean default false not null
);

alter table "order"."Restaurant"
    owner to myuser;

create table if not exists "order"."Shipper"
(
    "Id"     text                  not null
    constraint "PK_Shipper"
    primary key,
    "Name"   text                  not null,
    "Avatar" text                  not null,
    "IsFree" boolean default false not null
);

alter table "order"."Shipper"
    owner to myuser;

create table if not exists "order"."Order"
(
    "Id"              bigint generated by default as identity
    constraint "PK_Order"
    primary key,
    "OrderDate"       timestamp           not null,
    "ShippingFee"     numeric default 0.0 not null,
    "DeliveryInfo"    jsonb,
    "Status"          integer default 0   not null,
    "CustomerId"      text                not null
    constraint "FK_Order_Customer_CustomerId"
    references "order"."Customer"
    on delete restrict,
    "RestaurantId"    text                not null
    constraint "FK_Order_Restaurant_RestaurantId"
    references "order"."Restaurant"
    on delete restrict,
    "ShipperId"       text
    constraint "FK_Order_Shipper_ShipperId"
    references "order"."Shipper"
    on delete restrict,
    "PaymentMethodId" uuid
    constraint "FK_Order_PaymentMethod_PaymentMethodId"
    references "order"."PaymentMethod"
    on delete restrict
);

alter table "order"."Order"
    owner to myuser;

create index if not exists "IX_Order_CustomerId"
    on "order"."Order" ("CustomerId");

create index if not exists "IX_Order_PaymentMethodId"
    on "order"."Order" ("PaymentMethodId");

create index if not exists "IX_Order_RestaurantId"
    on "order"."Order" ("RestaurantId");

create index if not exists "IX_Order_ShipperId"
    on "order"."Order" ("ShipperId");

create table if not exists "order"."Category"
(
    "Id"    uuid         not null
    constraint "PK_Category"
    primary key,
    "Name"  varchar(100) not null,
    "Image" text         not null
    );

alter table "order"."Category"
    owner to myuser;

create table if not exists "order"."Food"
(
    "Id"           integer generated by default as identity
    constraint "PK_Food"
    primary key,
    "Name"         varchar                                                      not null,
    "Image"        text                                                         not null,
    "CategoryId"   uuid    default '00000000-0000-0000-0000-000000000000'::uuid not null
    constraint "FK_Food_Category_CategoryId"
    references "order"."Category"
    on delete restrict,
    "Description"  text    default ''::text                                     not null,
    "Price"        numeric default 0.0                                          not null,
    "RestaurantId" text    default ''::text                                     not null
    constraint "FK_Food_Restaurant_RestaurantId"
    references "order"."Restaurant"
    on delete restrict,
    "IsDeleted"    boolean default false                                        not null
);

alter table "order"."Food"
    owner to myuser;

create index if not exists "IX_Food_CategoryId"
    on "order"."Food" ("CategoryId");

create index if not exists "IX_Food_RestaurantId"
    on "order"."Food" ("RestaurantId");

create table if not exists "order"."OrderDetail"
(
    "Id"      bigint generated by default as identity
    constraint "PK_OrderDetail"
    primary key,
    "OrderId" bigint            not null
    constraint "FK_OrderDetail_Order_OrderId"
    references "order"."Order"
    on delete restrict,
    "FoodId"  integer           not null
    constraint "FK_OrderDetail_Food_FoodId"
    references "order"."Food"
    on delete restrict,
    "Amount"  integer           not null,
    "Price"   numeric           not null,
    "Status"  integer default 0 not null
);

alter table "order"."OrderDetail"
    owner to myuser;

create index if not exists "IX_OrderDetail_FoodId"
    on "order"."OrderDetail" ("FoodId");

create index if not exists "IX_OrderDetail_OrderId"
    on "order"."OrderDetail" ("OrderId");

create table if not exists "order"."Ingredient"
(
    "Id"           integer generated by default as identity
    constraint "PK_Ingredient"
    primary key,
    "Name"         varchar                      not null,
    "Image"        text                         not null,
    "Quantity"     double precision default 0.0 not null,
    "Unit"         integer                      not null,
    "RestaurantId" text                         not null
    constraint "FK_Ingredient_Restaurant_RestaurantId"
    references "order"."Restaurant"
    on delete restrict
);

alter table "order"."Ingredient"
    owner to myuser;

create index if not exists "IX_Ingredient_RestaurantId"
    on "order"."Ingredient" ("RestaurantId");

create table if not exists "order"."RequiredIngredient"
(
    "Id"           integer generated by default as identity
    constraint "PK_RequiredIngredient"
    primary key,
    "FoodId"       integer          not null
    constraint "FK_RequiredIngredient_Food_FoodId"
    references "order"."Food"
    on delete restrict,
    "IngredientId" integer          not null
    constraint "FK_RequiredIngredient_Ingredient_IngredientId"
    references "order"."Ingredient"
    on delete restrict,
    "Quantity"     double precision not null
);

alter table "order"."RequiredIngredient"
    owner to myuser;

create index if not exists "IX_RequiredIngredient_FoodId"
    on "order"."RequiredIngredient" ("FoodId");

create index if not exists "IX_RequiredIngredient_IngredientId"
    on "order"."RequiredIngredient" ("IngredientId");

create table if not exists "order"."Voucher"
(
    "Id"          bigint generated by default as identity
    constraint "PK_Voucher"
    primary key,
    "VoucherName" text                     not null,
    "Amount"      numeric                  not null,
    "Quantity"    integer                  not null,
    "StartDate"   timestamp with time zone not null,
    "EndDate"     timestamp with time zone not null,
    "Expired"     integer                  not null,
    "IsDeleted"   boolean                  not null
);

alter table "order"."Voucher"
    owner to myuser;

create table if not exists "order"."VoucherWallet"
(
    "Id"          bigint generated by default as identity
    constraint "PK_VoucherWallet"
    primary key,
    "Amount"      numeric                  not null,
    "StartDate"   timestamp with time zone not null,
    "ExpiredDate" timestamp with time zone not null,
    "CustomerId"  text                     not null
    constraint "FK_VoucherWallet_Customer_CustomerId"
    references "order"."Customer"
    on delete cascade
);

alter table "order"."VoucherWallet"
    owner to myuser;

create index if not exists "IX_VoucherWallet_CustomerId"
    on "order"."VoucherWallet" ("CustomerId");

create table if not exists "order"."NoshPointTransaction"
(
    "Id"              bigint generated by default as identity
    constraint "PK_NoshPointTransaction"
    primary key,
    "Amount"          numeric   not null,
    "CreatedDate"     timestamp not null,
    "TransactionType" integer   not null,
    "OrderId"         bigint    not null,
    "CustomerId"      text      not null
    constraint "FK_NoshPointTransaction_Customer_CustomerId"
    references "order"."Customer"
    on delete restrict,
    "VoucherId"       bigint
    constraint "FK_NoshPointTransaction_Voucher_VoucherId"
    references "order"."Voucher"
    on delete restrict
);

alter table "order"."NoshPointTransaction"
    owner to myuser;

create index if not exists "IX_NoshPointTransaction_CustomerId"
    on "order"."NoshPointTransaction" ("CustomerId");

create index if not exists "IX_NoshPointTransaction_VoucherId"
    on "order"."NoshPointTransaction" ("VoucherId");
