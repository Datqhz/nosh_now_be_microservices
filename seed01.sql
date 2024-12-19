
INSERT INTO auth."ApiResources" ("Name", "DisplayName") VALUES ('Mobile', 'Mobile');

INSERT INTO auth."ApiResourceScopes" ("Scope", "ApiResourceId") VALUES ('Admin', 1);
INSERT INTO auth."ApiResourceScopes" ("Scope", "ApiResourceId") VALUES ('Customer', 1);
INSERT INTO auth."ApiResourceScopes" ("Scope", "ApiResourceId") VALUES ('Restaurant', 1);
INSERT INTO auth."ApiResourceScopes" ("Scope", "ApiResourceId") VALUES ('ServiceStaff', 1);
INSERT INTO auth."ApiResourceScopes" ("Scope", "ApiResourceId") VALUES ('Chef', 1);

INSERT INTO auth."Clients" ("ClientId", "ClientName") VALUES ('Mobile', 'mobile');

INSERT INTO auth."ClientGrantTypes" ("ClientId", "GrantType") VALUES (1, 'password');
INSERT INTO auth."ClientGrantTypes" ("ClientId", "GrantType") VALUES (1, 'code');
INSERT INTO auth."ClientGrantTypes" ("ClientId", "GrantType") VALUES (1, 'client_credentials');

INSERT INTO auth."ClientScopes" ("Scope", "ClientId") VALUES ('Admin', 1);
INSERT INTO auth."ClientScopes" ("Scope", "ClientId") VALUES ('Customer', 1);
INSERT INTO auth."ClientScopes" ("Scope", "ClientId") VALUES ('Restaurant', 1);
INSERT INTO auth."ClientScopes" ("Scope", "ClientId") VALUES ('Chef', 1);
INSERT INTO auth."ClientScopes" ("Scope", "ClientId") VALUES ('ServiceStaff', 1);

INSERT INTO auth."ClientSecrets" ("SecretName", "ClientId") VALUES ('mobile', 1);

INSERT INTO auth."RolePermission" ("Permission", "Role") VALUES ('Admin', 'Admin');
INSERT INTO auth."RolePermission" ("Permission", "Role") VALUES ('Customer', 'Customer');
INSERT INTO auth."RolePermission" ("Permission", "Role") VALUES ('Restaurant', 'Restaurant');
INSERT INTO auth."RolePermission" ("Permission", "Role") VALUES ('ServiceStaff', 'ServiceStaff');
INSERT INTO auth."RolePermission" ("Permission", "Role") VALUES ('Chef', 'Chef');

INSERT INTO auth."AspNetRoles" ("Id", "Name", "NormalizedName", "ConcurrencyStamp") VALUES ('45bfea26-0391-48ce-95a2-41b1f16c2633', 'ServiceStaff', 'SERVICESTAFF', null);
INSERT INTO auth."AspNetRoles" ("Id", "Name", "NormalizedName", "ConcurrencyStamp") VALUES ('5a088ddf-b132-4269-9377-1711b06a2bc1', 'Restaurant', 'RESTAURANT', null);
INSERT INTO auth."AspNetRoles" ("Id", "Name", "NormalizedName", "ConcurrencyStamp") VALUES ('bf11bca0-6fdb-42cd-a19e-ec5f27c96865', 'Admin', 'ADMIN', null);
INSERT INTO auth."AspNetRoles" ("Id", "Name", "NormalizedName", "ConcurrencyStamp") VALUES ('cb0b1118-1a71-4e47-abef-abfd327c86fe', 'Chef', 'CHEF', null);
INSERT INTO auth."AspNetRoles" ("Id", "Name", "NormalizedName", "ConcurrencyStamp") VALUES ('cb11918a-8081-492a-9634-42b26b169f8e', 'Customer', 'CUSTOMER', null);

INSERT INTO auth."AspNetUsers" ("Id", "UserName", "NormalizedUserName", "Email", "NormalizedEmail", "EmailConfirmed", "PasswordHash", "SecurityStamp", "ConcurrencyStamp", "PhoneNumber", "PhoneNumberConfirmed", "TwoFactorEnabled", "LockoutEnd", "LockoutEnabled", "AccessFailedCount") VALUES ('444c34a6-435b-45e7-ac40-77a783a38f41', 'cc@yopmail.com', 'CC@YOPMAIL.COM', 'cc@yopmail.com', 'CC@YOPMAIL.COM', true, 'AQAAAAIAAYagAAAAEAYPk260PgcsCvGAHyfZ5NbRAdN9eB4QHZMuIJdC3ZN1/Ls4xqwj9TY1bGBGnjTYhw==', 'HAIDH7KC5D2LKV3RLZ2ZVQYJHBBZMVQA', '81626bdc-e11b-400e-a19c-cf63bce8e028', '0987654321', false, false, null, true, 0);
INSERT INTO auth."AspNetUsers" ("Id", "UserName", "NormalizedUserName", "Email", "NormalizedEmail", "EmailConfirmed", "PasswordHash", "SecurityStamp", "ConcurrencyStamp", "PhoneNumber", "PhoneNumberConfirmed", "TwoFactorEnabled", "LockoutEnd", "LockoutEnabled", "AccessFailedCount") VALUES ('8be65425-4108-4e96-816e-cda333d1297f', 'g@yopmail.com', 'G@YOPMAIL.COM', 'g@yopmail.com', 'G@YOPMAIL.COM', true, 'AQAAAAIAAYagAAAAEL2wJt3ACm45f9Z23Qqd32hWZPKunk/qD+i3vq7zjcLFMMdpP7Utqy2tSMal/L9twQ==', '7OFZLYLXFVUVXEJ6DO2HNPAXFFXOLWYM', 'c480e52a-aabc-48e0-bc57-e1f7240ee4e2', '0987654321', false, false, null, true, 0);
INSERT INTO auth."AspNetUsers" ("Id", "UserName", "NormalizedUserName", "Email", "NormalizedEmail", "EmailConfirmed", "PasswordHash", "SecurityStamp", "ConcurrencyStamp", "PhoneNumber", "PhoneNumberConfirmed", "TwoFactorEnabled", "LockoutEnd", "LockoutEnabled", "AccessFailedCount") VALUES ('8fd68682-c547-4170-99ed-af478a7ae952', 'dd@yopmail.com', 'DD@YOPMAIL.COM', 'dd@yopmail.com', 'DD@YOPMAIL.COM', true, 'AQAAAAIAAYagAAAAEKwwqAYdlN2+LXDGY+wZ309YnlYCev/KkWlnsAygR1Dp17jU4IBa4pplmc2/2r3SKw==', '2WZSQBEFHNVG23IVT2RRLJ4INV2SEA5X', '47ebca8b-5402-4bc1-8847-4a895c253581', '0987654321', false, false, null, true, 0);
INSERT INTO auth."AspNetUsers" ("Id", "UserName", "NormalizedUserName", "Email", "NormalizedEmail", "EmailConfirmed", "PasswordHash", "SecurityStamp", "ConcurrencyStamp", "PhoneNumber", "PhoneNumberConfirmed", "TwoFactorEnabled", "LockoutEnd", "LockoutEnabled", "AccessFailedCount") VALUES ('e966ec8e-f4ac-479d-879b-4ae611ec02b4', 'c@yopmail.com', 'C@YOPMAIL.COM', 'c@yopmail.com', 'C@YOPMAIL.COM', true, 'AQAAAAIAAYagAAAAEDOk+lfzLiEAZcfjpXmetoaL1H6sJXU6NnZD9MUNkkjLT1fv81U6WaHd6f3LOZWBOA==', 'R2Z6N75GYUK645HM2BBR2TG66XLS37G2', '9a529201-f22b-4eb9-879c-2570c3b992fb', '0987654321', false, false, null, true, 3);
INSERT INTO auth."AspNetUsers" ("Id", "UserName", "NormalizedUserName", "Email", "NormalizedEmail", "EmailConfirmed", "PasswordHash", "SecurityStamp", "ConcurrencyStamp", "PhoneNumber", "PhoneNumberConfirmed", "TwoFactorEnabled", "LockoutEnd", "LockoutEnabled", "AccessFailedCount") VALUES ('22874b30-0d7b-41c0-94db-82e100a7af9b', 'aa@yopmail.com', 'AA@YOPMAIL.COM', 'aa@yopmail.com', 'AA@YOPMAIL.COM', true, 'AQAAAAIAAYagAAAAEOhj354m2ew1iCTMMvhAaeJW73UzHJD3CnA6bnc68Ohuw9Ec/ByCVrsojv/uzIxhqw==', 'K62BGVPK4XJAXU26FHULSVDAZSTLYK57', '9229f0e5-f680-420a-84bb-09efec5cda95', '0987654321', false, false, null, true, 0);
INSERT INTO auth."AspNetUsers" ("Id", "UserName", "NormalizedUserName", "Email", "NormalizedEmail", "EmailConfirmed", "PasswordHash", "SecurityStamp", "ConcurrencyStamp", "PhoneNumber", "PhoneNumberConfirmed", "TwoFactorEnabled", "LockoutEnd", "LockoutEnabled", "AccessFailedCount") VALUES ('0d61703f-2c41-44fe-8558-eb3f44582ef6', 'a@yopmail.com', 'A@YOPMAIL.COM', 'a@yopmail.com', 'A@YOPMAIL.COM', true, 'AQAAAAIAAYagAAAAEM9F2hAq4zb4DQABcLQFVRNoRre9hgAhYilAS0H0gqtC2lnB8EwK/92+xnq0YUEgRQ==', '3N6GXGCORPFD5EMED3R6JSAQWFDD6RGI', '25905807-8c48-453e-ae85-3af5925c52d7', '0987654321', false, false, null, true, 0);
INSERT INTO auth."AspNetUsers" ("Id", "UserName", "NormalizedUserName", "Email", "NormalizedEmail", "EmailConfirmed", "PasswordHash", "SecurityStamp", "ConcurrencyStamp", "PhoneNumber", "PhoneNumberConfirmed", "TwoFactorEnabled", "LockoutEnd", "LockoutEnabled", "AccessFailedCount") VALUES ('ca8130b3-1bd3-4043-9937-ac634f2f928b', 'd@yopmail.com', 'D@YOPMAIL.COM', 'd@yopmail.com', 'D@YOPMAIL.COM', true, 'AQAAAAIAAYagAAAAEHNFdZFqSbdcUw59YTC7/oC2p1mFd73puMPSQygELvhTt6wG+FPDSTrhlCy9I4D4Zg==', 'GSWBKZPJA3GKLXO73Q6DKFQUZ3KMR4BO', '4c379ad9-052b-49e1-b828-f415c8963f40', '0987654321', false, false, null, true, 0);
INSERT INTO auth."AspNetUsers" ("Id", "UserName", "NormalizedUserName", "Email", "NormalizedEmail", "EmailConfirmed", "PasswordHash", "SecurityStamp", "ConcurrencyStamp", "PhoneNumber", "PhoneNumberConfirmed", "TwoFactorEnabled", "LockoutEnd", "LockoutEnabled", "AccessFailedCount") VALUES ('98024d0b-ee96-4e99-8fb1-44b5c3fbdd03', 'e@yopmail.com', 'E@YOPMAIL.COM', 'e@yopmail.com', 'E@YOPMAIL.COM', false, 'AQAAAAIAAYagAAAAEO4FBCEHEwBwDtCjRfCc0TNIXjNs3fwj9xXDMmkTpuTrAOlTpw3EZhzAYIc1NbnhkQ==', 'ZP3IYMCTQYW36645LTFHZMXQL3NVCK7D', '7f972595-c605-4fe7-adfd-46e69ff59ed2', '0987654321', false, false, null, true, 0);
INSERT INTO auth."AspNetUsers" ("Id", "UserName", "NormalizedUserName", "Email", "NormalizedEmail", "EmailConfirmed", "PasswordHash", "SecurityStamp", "ConcurrencyStamp", "PhoneNumber", "PhoneNumberConfirmed", "TwoFactorEnabled", "LockoutEnd", "LockoutEnabled", "AccessFailedCount") VALUES ('159a41bd-2d74-4d24-8f0d-53481383a365', 'f@gmail.com', 'F@GMAIL.COM', 'f@gmail.com', 'F@GMAIL.COM', false, 'AQAAAAIAAYagAAAAEG6ksHwp68uKzs58I8rK0DSFXF19oz73NztgB0VChyH31GJloOXxd0xuqbkbpXIh3A==', 'N3V3HB5L7IZJYJWLPAC4JROS5QPZCV6G', 'ede4dc74-74ee-4db2-91c2-8602eb309ed7', '0987654321', false, false, null, true, 0);
INSERT INTO auth."AspNetUsers" ("Id", "UserName", "NormalizedUserName", "Email", "NormalizedEmail", "EmailConfirmed", "PasswordHash", "SecurityStamp", "ConcurrencyStamp", "PhoneNumber", "PhoneNumberConfirmed", "TwoFactorEnabled", "LockoutEnd", "LockoutEnabled", "AccessFailedCount") VALUES ('e4f52e7b-4e69-4d59-99dc-a775733a3ee1', 'b@yopmail.com', 'B@YOPMAIL.COM', 'b@yopmail.com', 'B@YOPMAIL.COM', true, 'AQAAAAIAAYagAAAAEI6nYMb5HjAiAS9F37mkmrokPhiv+w00isMNIRGOwE8NiHZdLY3LGhshQ4LFJgHqmg==', 'J35FYPHSVHPSM55DAG5RD3PVKL42CG3F', '039e03e9-be15-43e7-8289-7ec3be17bc1f', '0987654321', false, false, null, true, 0);

INSERT INTO auth."Account" ("Id", "ClientId", "CreatedDate", "IsActive") VALUES ('ca8130b3-1bd3-4043-9937-ac634f2f928b', 1, '2024-12-15 17:58:44.742192', true);
INSERT INTO auth."Account" ("Id", "ClientId", "CreatedDate", "IsActive") VALUES ('98024d0b-ee96-4e99-8fb1-44b5c3fbdd03', 1, '2024-12-16 14:18:23.679241', false);
INSERT INTO auth."Account" ("Id", "ClientId", "CreatedDate", "IsActive") VALUES ('159a41bd-2d74-4d24-8f0d-53481383a365', 1, '2024-12-16 14:22:16.366663', false);
INSERT INTO auth."Account" ("Id", "ClientId", "CreatedDate", "IsActive") VALUES ('2d3136c6-cd6f-4534-82f8-ccf085c878f0', 1, '2024-12-16 14:25:08.372604', true);
INSERT INTO auth."Account" ("Id", "ClientId", "CreatedDate", "IsActive") VALUES ('8be65425-4108-4e96-816e-cda333d1297f', 1, '2024-12-16 14:26:12.097624', true);
INSERT INTO auth."Account" ("Id", "ClientId", "CreatedDate", "IsActive") VALUES ('22874b30-0d7b-41c0-94db-82e100a7af9b', 1, '2024-12-16 15:32:25.183190', true);
INSERT INTO auth."Account" ("Id", "ClientId", "CreatedDate", "IsActive") VALUES ('0c38a5a9-5b50-4b06-8a02-8c15a31c3368', 1, '2024-12-17 14:53:36.701029', true);
INSERT INTO auth."Account" ("Id", "ClientId", "CreatedDate", "IsActive") VALUES ('444c34a6-435b-45e7-ac40-77a783a38f41', 1, '2024-12-17 14:59:03.612299', true);
INSERT INTO auth."Account" ("Id", "ClientId", "CreatedDate", "IsActive") VALUES ('8fd68682-c547-4170-99ed-af478a7ae952', 1, '2024-12-17 15:04:03.374757', true);
INSERT INTO auth."Account" ("Id", "ClientId", "CreatedDate", "IsActive") VALUES ('99d50965-1808-4c8f-9892-fd3bc8c9e1ed', 1, '2024-12-17 18:33:15.560734', true);

INSERT INTO auth."AspNetUserRoles" ("UserId", "RoleId") VALUES ('0d61703f-2c41-44fe-8558-eb3f44582ef6', '5a088ddf-b132-4269-9377-1711b06a2bc1');
INSERT INTO auth."AspNetUserRoles" ("UserId", "RoleId") VALUES ('e4f52e7b-4e69-4d59-99dc-a775733a3ee1', '5a088ddf-b132-4269-9377-1711b06a2bc1');
INSERT INTO auth."AspNetUserRoles" ("UserId", "RoleId") VALUES ('4738abfb-1b4e-4147-a3c9-04bd31f72379', 'cb11918a-8081-492a-9634-42b26b169f8e');
INSERT INTO auth."AspNetUserRoles" ("UserId", "RoleId") VALUES ('e966ec8e-f4ac-479d-879b-4ae611ec02b4', 'cb11918a-8081-492a-9634-42b26b169f8e');
INSERT INTO auth."AspNetUserRoles" ("UserId", "RoleId") VALUES ('ca8130b3-1bd3-4043-9937-ac634f2f928b', 'cb11918a-8081-492a-9634-42b26b169f8e');
INSERT INTO auth."AspNetUserRoles" ("UserId", "RoleId") VALUES ('98024d0b-ee96-4e99-8fb1-44b5c3fbdd03', '45bfea26-0391-48ce-95a2-41b1f16c2633');
INSERT INTO auth."AspNetUserRoles" ("UserId", "RoleId") VALUES ('159a41bd-2d74-4d24-8f0d-53481383a365', 'cb0b1118-1a71-4e47-abef-abfd327c86fe');
INSERT INTO auth."AspNetUserRoles" ("UserId", "RoleId") VALUES ('2d3136c6-cd6f-4534-82f8-ccf085c878f0', 'cb0b1118-1a71-4e47-abef-abfd327c86fe');
INSERT INTO auth."AspNetUserRoles" ("UserId", "RoleId") VALUES ('8be65425-4108-4e96-816e-cda333d1297f', '45bfea26-0391-48ce-95a2-41b1f16c2633');
INSERT INTO auth."AspNetUserRoles" ("UserId", "RoleId") VALUES ('22874b30-0d7b-41c0-94db-82e100a7af9b', 'cb11918a-8081-492a-9634-42b26b169f8e');


INSERT INTO core."Customer" ("Id", "DisplayName", "Email", "PhoneNumber", "IsActive", "Avatar", "BoomCount") VALUES ('4738abfb-1b4e-4147-a3c9-04bd31f72379', 'Quốc Đạt', 'c@yopmaill.com', '0987654321', false, '', 0);
INSERT INTO core."Customer" ("Id", "DisplayName", "Email", "PhoneNumber", "IsActive", "Avatar", "BoomCount") VALUES ('e966ec8e-f4ac-479d-879b-4ae611ec02b4', 'Quốc Đạt', 'c@yopmail.com', '0987654321', true, '', 0);
INSERT INTO core."Customer" ("Id", "DisplayName", "Email", "PhoneNumber", "IsActive", "Avatar", "BoomCount") VALUES ('ca8130b3-1bd3-4043-9937-ac634f2f928b', 'Quốc Đạt', 'd@yopmail.com', '0987654321', true, '', 0);
INSERT INTO core."Customer" ("Id", "DisplayName", "Email", "PhoneNumber", "IsActive", "Avatar", "BoomCount") VALUES ('22874b30-0d7b-41c0-94db-82e100a7af9b', 'Hồ Đạt', 'aa@yopmail.com', '0987654321', true, '', 0);
INSERT INTO core."Customer" ("Id", "DisplayName", "Email", "PhoneNumber", "IsActive", "Avatar", "BoomCount") VALUES ('0c38a5a9-5b50-4b06-8a02-8c15a31c3368', 'Lê Thị Vân', 'bb@yopmail.com', '0987654321', true, '', 0);
INSERT INTO core."Customer" ("Id", "DisplayName", "Email", "PhoneNumber", "IsActive", "Avatar", "BoomCount") VALUES ('444c34a6-435b-45e7-ac40-77a783a38f41', 'Đường Long', 'cc@yopmail.com', '0987654321', true, '', 0);
INSERT INTO core."Customer" ("Id", "DisplayName", "Email", "PhoneNumber", "IsActive", "Avatar", "BoomCount") VALUES ('8fd68682-c547-4170-99ed-af478a7ae952', 'Long Thạnh Mĩ', 'dd@yopmail.com', '0987654321', true, '', 0);
INSERT INTO core."Customer" ("Id", "DisplayName", "Email", "PhoneNumber", "IsActive", "Avatar", "BoomCount") VALUES ('99d50965-1808-4c8f-9892-fd3bc8c9e1ed', 'tédt', 'ee@yopmail.com', '0987654321', true, 'http://res.cloudinary.com/dyhjqna2u/image/upload/v1734460394/avatar/qjxkqk5aaw3jwozqchcs.jpg', 0);

INSERT INTO core."Restaurant" ("Id", "DisplayName", "Email", "PhoneNumber", "IsActive", "Avatar", "Coordinate") VALUES ('e4f52e7b-4e69-4d59-99dc-a775733a3ee1', 'Tocotoco', 'b@yopmail.com', '0987654321', true, '', '10.8479126051153-106.77566494766673');
INSERT INTO core."Restaurant" ("Id", "DisplayName", "Email", "PhoneNumber", "IsActive", "Avatar", "Coordinate") VALUES ('0d61703f-2c41-44fe-8558-eb3f44582ef6', 'Pho 10 Ly Quoc Su', 'a@yopmail.com', '0987654321', true, 'http://res.cloudinary.com/dyhjqna2u/image/upload/v1734279917/avatar/wy4hptsqfebdgqkcppgp.jpg', '10.851251046075598-106.75531215944393');

INSERT INTO core."Employee" ("Id", "DisplayName", "Email", "PhoneNumber", "IsActive", "Avatar", "Role", "RestaurantId") VALUES ('98024d0b-ee96-4e99-8fb1-44b5c3fbdd03', 'Minh Nguyễn', 'e@yopmail.com', '0987654321', false, 'https://static.vecteezy.com/system/resources/previews/009/292/244/original/default-avatar-icon-of-social-media-user-vector.jpg', 0, '0d61703f-2c41-44fe-8558-eb3f44582ef6');
INSERT INTO core."Employee" ("Id", "DisplayName", "Email", "PhoneNumber", "IsActive", "Avatar", "Role", "RestaurantId") VALUES ('159a41bd-2d74-4d24-8f0d-53481383a365', 'Lê Thị Mi', 'f@gmail.com', '0987654321', false, 'https://static.vecteezy.com/system/resources/previews/009/292/244/original/default-avatar-icon-of-social-media-user-vector.jpg', 1, '0d61703f-2c41-44fe-8558-eb3f44582ef6');
INSERT INTO core."Employee" ("Id", "DisplayName", "Email", "PhoneNumber", "IsActive", "Avatar", "Role", "RestaurantId") VALUES ('2d3136c6-cd6f-4534-82f8-ccf085c878f0', 'Nguyễn Trung Trực', 'f@yopmail.com', '0987654321', true, 'https://static.vecteezy.com/system/resources/previews/009/292/244/original/default-avatar-icon-of-social-media-user-vector.jpg', 1, '0d61703f-2c41-44fe-8558-eb3f44582ef6');
INSERT INTO core."Employee" ("Id", "DisplayName", "Email", "PhoneNumber", "IsActive", "Avatar", "Role", "RestaurantId") VALUES ('8be65425-4108-4e96-816e-cda333d1297f', 'Trần Thị Trà My', 'g@yopmail.com', '0987654322', true, 'https://static.vecteezy.com/system/resources/previews/009/292/244/original/default-avatar-icon-of-social-media-user-vector.jpg', 0, '0d61703f-2c41-44fe-8558-eb3f44582ef6');

INSERT INTO core."Location" ("Name", "Phone", "Coordinate", "CustomerId") VALUES ('Home', '0987878654', '10.854519578496134-106.7589907209082', '8fd68682-c547-4170-99ed-af478a7ae952');
INSERT INTO core."Location" ("Name", "Phone", "Coordinate", "CustomerId") VALUES ('my location', '0987654321', '10.858585056127364-106.75870124699297', '8fd68682-c547-4170-99ed-af478a7ae952');
INSERT INTO core."Location" ("Name", "Phone", "Coordinate", "CustomerId") VALUES ('Default', '0987654321', '10.853273929912566-106.75196503655351', '99d50965-1808-4c8f-9892-fd3bc8c9e1ed');

INSERT INTO core."Calendar" ("RestaurantId", "EndTime", "StartTime") VALUES ('0d61703f-2c41-44fe-8558-eb3f44582ef6', '2024-12-18 22:30:00.000000', '2024-12-18 08:30:00.000000');
INSERT INTO core."Calendar" ("RestaurantId", "EndTime", "StartTime") VALUES ('0d61703f-2c41-44fe-8558-eb3f44582ef6', '2024-12-19 22:30:00.000000', '2024-12-19 08:30:00.000000');
INSERT INTO core."Calendar" ("RestaurantId", "EndTime", "StartTime") VALUES ('0d61703f-2c41-44fe-8558-eb3f44582ef6', '2024-12-20 22:30:00.000000', '2024-12-20 08:30:00.000000');
INSERT INTO core."Calendar" ("RestaurantId", "EndTime", "StartTime") VALUES ('0d61703f-2c41-44fe-8558-eb3f44582ef6', '2024-12-21 22:30:00.000000', '2024-12-21 08:30:00.000000');
INSERT INTO core."Calendar" ("RestaurantId", "EndTime", "StartTime") VALUES ('0d61703f-2c41-44fe-8558-eb3f44582ef6', '2024-12-22 22:30:00.000000', '2024-12-22 08:30:00.000000');
INSERT INTO core."Calendar" ("RestaurantId", "EndTime", "StartTime") VALUES ('0d61703f-2c41-44fe-8558-eb3f44582ef6', '2024-12-17 23:30:00.000000', '2024-12-17 08:30:00.000000');


INSERT INTO "order"."Category" ("Id", "Name", "Image") VALUES ('f6eda93d-722c-41cc-b515-51dd3df5d4b9', 'Milk tea', 'http://res.cloudinary.com/dyhjqna2u/image/upload/v1734279757/category/qbohna1mbrerzaawjffe.jpg');
INSERT INTO "order"."Category" ("Id", "Name", "Image") VALUES ('f25626cb-dde0-419e-a636-204bc21fccf2', 'Fast food', 'http://res.cloudinary.com/dyhjqna2u/image/upload/v1734279784/category/lujjrln9lp3dcui6datk.jpg');
INSERT INTO "order"."Category" ("Id", "Name", "Image") VALUES ('ae8ad84c-c0d1-4040-af9f-108a9c8ba38d', 'Noodles', 'http://res.cloudinary.com/dyhjqna2u/image/upload/v1734279801/category/bjob4pd8jkjzpglvacar.jpg');

INSERT INTO "order"."PaymentMethod" ("Id", "Name", "Image") VALUES ('a527b118-fa42-4cca-b72e-5c71b4642d62', 'Cash', 'https://res.cloudinary.com/dyhjqna2u/image/upload/v1732556257/cash_dggz5g.png');
INSERT INTO "order"."PaymentMethod" ("Id", "Name", "Image") VALUES ('a527b118-fa42-4cca-b72e-5c71b4642d63', 'Momo', 'https://res.cloudinary.com/dyhjqna2u/image/upload/v1732556257/momo_mm1eu0.png');
INSERT INTO "order"."PaymentMethod" ("Id", "Name", "Image") VALUES ('a527b118-fa42-4cca-b72e-5c71b4642d64', 'GPay', 'https://res.cloudinary.com/dyhjqna2u/image/upload/v1732556258/google-pay-icon_n1dqti.png');

INSERT INTO "order"."Customer" ("Id", "Name", "Avatar", "BoomCount", "NoshPoint") VALUES ('4738abfb-1b4e-4147-a3c9-04bd31f72379', 'Quốc Đạt', '', 0, 0);
INSERT INTO "order"."Customer" ("Id", "Name", "Avatar", "BoomCount", "NoshPoint") VALUES ('e966ec8e-f4ac-479d-879b-4ae611ec02b4', 'Quốc Đạt', '', 0, 0);
INSERT INTO "order"."Customer" ("Id", "Name", "Avatar", "BoomCount", "NoshPoint") VALUES ('ca8130b3-1bd3-4043-9937-ac634f2f928b', 'Quốc Đạt', '', 0, 0);
INSERT INTO "order"."Customer" ("Id", "Name", "Avatar", "BoomCount", "NoshPoint") VALUES ('22874b30-0d7b-41c0-94db-82e100a7af9b', 'Hồ Đạt', '', 0, 0);
INSERT INTO "order"."Customer" ("Id", "Name", "Avatar", "BoomCount", "NoshPoint") VALUES ('0c38a5a9-5b50-4b06-8a02-8c15a31c3368', 'Lê Thị Vân', '', 0, 0);
INSERT INTO "order"."Customer" ("Id", "Name", "Avatar", "BoomCount", "NoshPoint") VALUES ('444c34a6-435b-45e7-ac40-77a783a38f41', 'Đường Long', '', 0, 0);
INSERT INTO "order"."Customer" ("Id", "Name", "Avatar", "BoomCount", "NoshPoint") VALUES ('8fd68682-c547-4170-99ed-af478a7ae952', 'Long Thạnh Mĩ', '', 0, 40000);
INSERT INTO "order"."Customer" ("Id", "Name", "Avatar", "BoomCount", "NoshPoint") VALUES ('99d50965-1808-4c8f-9892-fd3bc8c9e1ed', 'tédt', 'http://res.cloudinary.com/dyhjqna2u/image/upload/v1734460394/avatar/qjxkqk5aaw3jwozqchcs.jpg', 0, 125000);

INSERT INTO "order"."Restaurant" ("Id", "Name", "Coordinate", "Phone", "Avatar", "IsActive") VALUES ('e4f52e7b-4e69-4d59-99dc-a775733a3ee1', 'Tocotoco', '10.8479126051153-106.77566494766673', '0987654321', '', true);
INSERT INTO "order"."Restaurant" ("Id", "Name", "Coordinate", "Phone", "Avatar", "IsActive") VALUES ('0d61703f-2c41-44fe-8558-eb3f44582ef6', 'Pho 10 Ly Quoc Su', '10.851251046075598-106.75531215944393', '0987654321', 'http://res.cloudinary.com/dyhjqna2u/image/upload/v1734279917/avatar/wy4hptsqfebdgqkcppgp.jpg', true);

INSERT INTO "order"."Employee" ("Id", "Name", "Avatar", "Role", "RestaurantId") VALUES ('98024d0b-ee96-4e99-8fb1-44b5c3fbdd03', 'Minh Nguyễn', 'https://static.vecteezy.com/system/resources/previews/009/292/244/original/default-avatar-icon-of-social-media-user-vector.jpg', 0, '0d61703f-2c41-44fe-8558-eb3f44582ef6');
INSERT INTO "order"."Employee" ("Id", "Name", "Avatar", "Role", "RestaurantId") VALUES ('159a41bd-2d74-4d24-8f0d-53481383a365', 'Lê Thị Mi', 'https://static.vecteezy.com/system/resources/previews/009/292/244/original/default-avatar-icon-of-social-media-user-vector.jpg', 1, '0d61703f-2c41-44fe-8558-eb3f44582ef6');
INSERT INTO "order"."Employee" ("Id", "Name", "Avatar", "Role", "RestaurantId") VALUES ('2d3136c6-cd6f-4534-82f8-ccf085c878f0', 'Nguyễn Trung Trực', 'https://static.vecteezy.com/system/resources/previews/009/292/244/original/default-avatar-icon-of-social-media-user-vector.jpg', 1, '0d61703f-2c41-44fe-8558-eb3f44582ef6');
INSERT INTO "order"."Employee" ("Id", "Name", "Avatar", "Role", "RestaurantId") VALUES ('8be65425-4108-4e96-816e-cda333d1297f', 'Trần Thị Trà My', 'https://static.vecteezy.com/system/resources/previews/009/292/244/original/default-avatar-icon-of-social-media-user-vector.jpg', 0, '0d61703f-2c41-44fe-8558-eb3f44582ef6');

INSERT INTO "order"."Food" ("Name", "Image", "CategoryId", "Description", "Price", "RestaurantId", "IsDeleted") VALUES ('Spaghetti', 'http://res.cloudinary.com/dyhjqna2u/image/upload/v1734284878/food/rp8tgeciflrnmlvusd79.jpg', 'ae8ad84c-c0d1-4040-af9f-108a9c8ba38d', 'It is a staple food of traditional Italian cuisine. Like other pasta, spaghetti is made of milled wheat, water, and sometimes enriched with vitamins and minerals. Italian spaghetti is typically made from durum-wheat semolina.', 54000, '0d61703f-2c41-44fe-8558-eb3f44582ef6', false);
INSERT INTO "order"."Food" ("Name", "Image", "CategoryId", "Description", "Price", "RestaurantId", "IsDeleted") VALUES ('Pho', 'http://res.cloudinary.com/dyhjqna2u/image/upload/v1734284988/food/n69ttywwkskmhggnthhf.jpg', 'ae8ad84c-c0d1-4040-af9f-108a9c8ba38d', 'Pho is a Vietnamese soup consisting of bone broth, rice noodles, and thinly sliced meat (usually beef). It may also be served with bean sprouts, fresh herbs, limes, chiles, and other garnishes.', 40000, '0d61703f-2c41-44fe-8558-eb3f44582ef6', false);
INSERT INTO "order"."Food" ("Name", "Image", "CategoryId", "Description", "Price", "RestaurantId", "IsDeleted") VALUES ('Hamburger', 'http://res.cloudinary.com/dyhjqna2u/image/upload/v1734358210/food/ps24615o7l7jjp2jzrts.jpg', 'f25626cb-dde0-419e-a636-204bc21fccf2', 'Hamburgers are quickly served and easily eaten. This convenience has made them a favorite food for generations of Americans. If not obtained from a grocer, it is served at diners and fast-food outlets and also sold in stalls at games, beaches, and other outdoor locales.', 34000, '0d61703f-2c41-44fe-8558-eb3f44582ef6', false);

INSERT INTO "order"."Ingredient" ("Name", "Image", "Quantity", "Unit", "RestaurantId") VALUES ('Tomato', 'http://res.cloudinary.com/dyhjqna2u/image/upload/v1734284392/ingredient/w5i337d7krpu34mmxxs8.jpg', 4.900000000000001, 2, '0d61703f-2c41-44fe-8558-eb3f44582ef6');
INSERT INTO "order"."Ingredient" ("Name", "Image", "Quantity", "Unit", "RestaurantId") VALUES ('Onion', 'http://res.cloudinary.com/dyhjqna2u/image/upload/v1734284178/ingredient/wwdprwpqkpjtb1voy84r.jpg', 45800, 1, '0d61703f-2c41-44fe-8558-eb3f44582ef6');
INSERT INTO "order"."Ingredient" ("Name", "Image", "Quantity", "Unit", "RestaurantId") VALUES ('Sugar', 'http://res.cloudinary.com/dyhjqna2u/image/upload/v1734284697/ingredient/shjhraaonrcrctwatqlk.jpg', 1.5599999999999985, 2, '0d61703f-2c41-44fe-8558-eb3f44582ef6');
INSERT INTO "order"."Ingredient" ("Name", "Image", "Quantity", "Unit", "RestaurantId") VALUES ('Noodles', 'http://res.cloudinary.com/dyhjqna2u/image/upload/v1734284726/ingredient/q90dkmhp7iv5mocbbghg.jpg', 0, 1, '0d61703f-2c41-44fe-8558-eb3f44582ef6');

INSERT INTO "order"."NoshPointTransaction" ("Amount", "CreatedDate", "TransactionType", "OrderId", "CustomerId", "VoucherId") VALUES (27000, '2024-12-18 17:12:59.515522', 0, 55, '99d50965-1808-4c8f-9892-fd3bc8c9e1ed', null);
INSERT INTO "order"."NoshPointTransaction" ("Amount", "CreatedDate", "TransactionType", "OrderId", "CustomerId", "VoucherId") VALUES (40000, '2024-12-18 17:15:36.706802', 0, 52, '8fd68682-c547-4170-99ed-af478a7ae952', null);
INSERT INTO "order"."NoshPointTransaction" ("Amount", "CreatedDate", "TransactionType", "OrderId", "CustomerId", "VoucherId") VALUES (27000, '2024-12-18 18:21:55.990482', 0, 62, '99d50965-1808-4c8f-9892-fd3bc8c9e1ed', null);
INSERT INTO "order"."NoshPointTransaction" ("Amount", "CreatedDate", "TransactionType", "OrderId", "CustomerId", "VoucherId") VALUES (17000, '2024-12-18 18:22:00.294881', 0, 63, '99d50965-1808-4c8f-9892-fd3bc8c9e1ed', null);
INSERT INTO "order"."NoshPointTransaction" ("Amount", "CreatedDate", "TransactionType", "OrderId", "CustomerId", "VoucherId") VALUES (54000, '2024-12-18 18:22:53.022189', 0, 64, '99d50965-1808-4c8f-9892-fd3bc8c9e1ed', null);


INSERT INTO "order"."Order" ("OrderDate", "ShippingFee", "DeliveryInfo", "Status", "CustomerId", "RestaurantId", "ShipperId", "PaymentMethodId") VALUES ('2024-12-18 16:50:02.119945', 15000, '{"Phone": "0987654321", "Coordinate": "10.853273929912566-106.75196503655351"}', 6, '99d50965-1808-4c8f-9892-fd3bc8c9e1ed', '0d61703f-2c41-44fe-8558-eb3f44582ef6', null, 'a527b118-fa42-4cca-b72e-5c71b4642d64');
INSERT INTO "order"."Order" ("OrderDate", "ShippingFee", "DeliveryInfo", "Status", "CustomerId", "RestaurantId", "ShipperId", "PaymentMethodId") VALUES ('2024-12-18 17:52:41.309754', 15000, '{"Phone": "0987654321", "Coordinate": "10.853273929912566-106.75196503655351"}', 3, '99d50965-1808-4c8f-9892-fd3bc8c9e1ed', '0d61703f-2c41-44fe-8558-eb3f44582ef6', null, 'a527b118-fa42-4cca-b72e-5c71b4642d62');
INSERT INTO "order"."Order" ("OrderDate", "ShippingFee", "DeliveryInfo", "Status", "CustomerId", "RestaurantId", "ShipperId", "PaymentMethodId") VALUES ('2024-12-17 15:17:25.203300', 15000, '{"Phone": "0987654321", "Coordinate": "10.858585056127364-106.75870124699297"}', 6, '8fd68682-c547-4170-99ed-af478a7ae952', '0d61703f-2c41-44fe-8558-eb3f44582ef6', null, 'a527b118-fa42-4cca-b72e-5c71b4642d64');
INSERT INTO "order"."Order" ("OrderDate", "ShippingFee", "DeliveryInfo", "Status", "CustomerId", "RestaurantId", "ShipperId", "PaymentMethodId") VALUES ('2024-12-18 17:28:17.984616', 15000, '{"Phone": "0987654321", "Coordinate": "10.853273929912566-106.75196503655351"}', 3, '99d50965-1808-4c8f-9892-fd3bc8c9e1ed', '0d61703f-2c41-44fe-8558-eb3f44582ef6', null, 'a527b118-fa42-4cca-b72e-5c71b4642d64');
INSERT INTO "order"."Order" ("OrderDate", "ShippingFee", "DeliveryInfo", "Status", "CustomerId", "RestaurantId", "ShipperId", "PaymentMethodId") VALUES ('2024-12-17 16:24:14.697750', 15000, '{"Phone": "0987878654", "Coordinate": "10.854519578496134-106.7589907209082"}', 9, '8fd68682-c547-4170-99ed-af478a7ae952', '0d61703f-2c41-44fe-8558-eb3f44582ef6', null, 'a527b118-fa42-4cca-b72e-5c71b4642d64');
INSERT INTO "order"."Order" ("OrderDate", "ShippingFee", "DeliveryInfo", "Status", "CustomerId", "RestaurantId", "ShipperId", "PaymentMethodId") VALUES ('2024-12-15 17:59:33.432188', 0, null, 0, 'ca8130b3-1bd3-4043-9937-ac634f2f928b', '0d61703f-2c41-44fe-8558-eb3f44582ef6', null, null);
INSERT INTO "order"."Order" ("OrderDate", "ShippingFee", "DeliveryInfo", "Status", "CustomerId", "RestaurantId", "ShipperId", "PaymentMethodId") VALUES ('2024-12-16 15:52:58.531125', 0, null, 0, '22874b30-0d7b-41c0-94db-82e100a7af9b', 'e4f52e7b-4e69-4d59-99dc-a775733a3ee1', null, null);
INSERT INTO "order"."Order" ("OrderDate", "ShippingFee", "DeliveryInfo", "Status", "CustomerId", "RestaurantId", "ShipperId", "PaymentMethodId") VALUES ('2024-12-16 15:59:32.344205', 0, null, 0, '22874b30-0d7b-41c0-94db-82e100a7af9b', '0d61703f-2c41-44fe-8558-eb3f44582ef6', null, null);
INSERT INTO "order"."Order" ("OrderDate", "ShippingFee", "DeliveryInfo", "Status", "CustomerId", "RestaurantId", "ShipperId", "PaymentMethodId") VALUES ('2024-12-18 17:38:09.395074', 15000, '{"Phone": "0987654321", "Coordinate": "10.853273929912566-106.75196503655351"}', 3, '99d50965-1808-4c8f-9892-fd3bc8c9e1ed', '0d61703f-2c41-44fe-8558-eb3f44582ef6', null, 'a527b118-fa42-4cca-b72e-5c71b4642d62');
INSERT INTO "order"."Order" ("OrderDate", "ShippingFee", "DeliveryInfo", "Status", "CustomerId", "RestaurantId", "ShipperId", "PaymentMethodId") VALUES ('2024-12-17 16:34:20.085774', 0, null, 0, '8fd68682-c547-4170-99ed-af478a7ae952', '0d61703f-2c41-44fe-8558-eb3f44582ef6', null, null);

INSERT INTO "order"."OrderDetail" ("OrderId", "FoodId", "Amount", "Price", "Status") VALUES (51, 88, 3, 0, 0);
INSERT INTO "order"."OrderDetail" ("OrderId", "FoodId", "Amount", "Price", "Status") VALUES (53, 88, 2, 40000, 0);
INSERT INTO "order"."OrderDetail" ("OrderId", "FoodId", "Amount", "Price", "Status") VALUES (53, 87, 2, 54000, 0);
INSERT INTO "order"."OrderDetail" ("OrderId", "FoodId", "Amount", "Price", "Status") VALUES (54, 88, 3, 0, 0);
INSERT INTO "order"."OrderDetail" ("OrderId", "FoodId", "Amount", "Price", "Status") VALUES (56, 89, 3, 34000, 1);
INSERT INTO "order"."OrderDetail" ("OrderId", "FoodId", "Amount", "Price", "Status") VALUES (55, 87, 1, 54000, 1);
INSERT INTO "order"."OrderDetail" ("OrderId", "FoodId", "Amount", "Price", "Status") VALUES (52, 88, 2, 40000, 1);
INSERT INTO "order"."OrderDetail" ("OrderId", "FoodId", "Amount", "Price", "Status") VALUES (57, 89, 1, 34000, 1);
INSERT INTO "order"."OrderDetail" ("OrderId", "FoodId", "Amount", "Price", "Status") VALUES (58, 89, 1, 34000, 1);
INSERT INTO "order"."OrderDetail" ("OrderId", "FoodId", "Amount", "Price", "Status") VALUES (59, 89, 2, 34000, 1);

INSERT INTO "order"."RequiredIngredient" ("FoodId", "IngredientId", "Quantity") VALUES (87, 51, 200);
INSERT INTO "order"."RequiredIngredient" ("FoodId", "IngredientId", "Quantity") VALUES (87, 55, 800);
INSERT INTO "order"."RequiredIngredient" ("FoodId", "IngredientId", "Quantity") VALUES (87, 54, 0.05);
INSERT INTO "order"."RequiredIngredient" ("FoodId", "IngredientId", "Quantity") VALUES (88, 55, 900);
INSERT INTO "order"."RequiredIngredient" ("FoodId", "IngredientId", "Quantity") VALUES (88, 54, 0.02);
INSERT INTO "order"."RequiredIngredient" ("FoodId", "IngredientId", "Quantity") VALUES (89, 51, 200);
INSERT INTO "order"."RequiredIngredient" ("FoodId", "IngredientId", "Quantity") VALUES (89, 52, 0.01);
INSERT INTO "order"."RequiredIngredient" ("FoodId", "IngredientId", "Quantity") VALUES (89, 54, 0.8);

INSERT INTO "order"."Shipper" ("Id", "Name", "Avatar", "IsFree") VALUES ('78673caf-9db3-7024-2b1f-75ffc35f10bd', 'Lamont Beahan', 'https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/483.jpg', true);
INSERT INTO "order"."Shipper" ("Id", "Name", "Avatar", "IsFree") VALUES ('fde0de30-f0d3-db2e-3499-0d67498763f3', 'Cyril Tromp', 'https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/446.jpg', true);
INSERT INTO "order"."Shipper" ("Id", "Name", "Avatar", "IsFree") VALUES ('82c6cba5-f582-135f-5f80-7e2300dbd493', 'Reva Beatty', 'https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/1165.jpg', true);
INSERT INTO "order"."Shipper" ("Id", "Name", "Avatar", "IsFree") VALUES ('767baa54-39f8-53e8-4589-55af02ad8a24', 'Reanna Brakus', 'https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/1135.jpg', true);
INSERT INTO "order"."Shipper" ("Id", "Name", "Avatar", "IsFree") VALUES ('ab9949cd-d42e-051f-2c31-8ab63d886b5c', 'Kiana Kuhn', 'https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/1012.jpg', true);
INSERT INTO "order"."Shipper" ("Id", "Name", "Avatar", "IsFree") VALUES ('d24b1f18-febe-3f7e-0f52-50a2d933d495', 'King Morissette', 'https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/381.jpg', true);
INSERT INTO "order"."Shipper" ("Id", "Name", "Avatar", "IsFree") VALUES ('16afea5f-e96c-6e62-a6c3-b058fffad09a', 'Lavina Cartwright', 'https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/522.jpg', true);
INSERT INTO "order"."Shipper" ("Id", "Name", "Avatar", "IsFree") VALUES ('768668b8-43a3-a9fb-92ec-81bb3982cb26', 'Mabel Koch', 'https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/340.jpg', true);
INSERT INTO "order"."Shipper" ("Id", "Name", "Avatar", "IsFree") VALUES ('dad3388a-4b47-5526-7434-bfffea3c78a9', 'Stella Beer', 'https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/1174.jpg', true);
INSERT INTO "order"."Shipper" ("Id", "Name", "Avatar", "IsFree") VALUES ('176ce7c3-56ea-8a0c-f849-8633aa27d74b', 'Marianna Haley', 'https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/297.jpg', true);


