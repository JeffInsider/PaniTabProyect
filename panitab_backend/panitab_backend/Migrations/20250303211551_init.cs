using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace panitab_backend.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.EnsureSchema(
                name: "Security");

            migrationBuilder.CreateTable(
                name: "baker",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    identity_number = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    first_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    last_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    balance = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    is_active = table.Column<bool>(type: "bit", nullable: false),
                    created_by = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_by = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    updated_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_baker", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "bread_class",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    category = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    trays_per_quintal = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    breads_per_tray = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    breads_per_bag = table.Column<int>(type: "int", nullable: false),
                    price_per_quintal = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    packaging_price = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    customer_price = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    public_price = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    is_active = table.Column<bool>(type: "bit", nullable: false),
                    created_by = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_by = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    updated_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bread_class", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "material",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    category = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    current_stock = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    measure_unit = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    is_active = table.Column<bool>(type: "bit", nullable: false),
                    created_by = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_by = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    updated_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_material", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "packer",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    identity_number = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    first_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    last_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    is_active = table.Column<bool>(type: "bit", nullable: false),
                    created_by = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_by = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    updated_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_packer", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                schema: "Security",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "supplier",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    contact = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    phone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    balance = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    created_by = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_by = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    updated_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_supplier", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                schema: "Security",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    first_name = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    last_name = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    is_active = table.Column<bool>(type: "bit", nullable: false),
                    refresh_token = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    refresh_token_expire = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "baker_payment",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    baker_id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 50, nullable: false),
                    payment_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    total_arrobas = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    amount_paid = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    payment_completed = table.Column<bool>(type: "bit", nullable: false),
                    balance_adjustment = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    last_production_paid = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    created_by = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_by = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    updated_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_baker_payment", x => x.id);
                    table.ForeignKey(
                        name: "FK_baker_payment_baker_baker_id",
                        column: x => x.baker_id,
                        principalSchema: "dbo",
                        principalTable: "baker",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "bread_class_materials",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    bread_class_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    material_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    quantity_used = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    measure_unit = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    created_by = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_by = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    updated_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bread_class_materials", x => x.id);
                    table.ForeignKey(
                        name: "FK_bread_class_materials_bread_class_bread_class_id",
                        column: x => x.bread_class_id,
                        principalSchema: "dbo",
                        principalTable: "bread_class",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_bread_class_materials_material_material_id",
                        column: x => x.material_id,
                        principalSchema: "dbo",
                        principalTable: "material",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "unit_conversion",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    material_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    from_unit = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    to_unit = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    conversion_factor = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    created_by = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_by = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    updated_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_unit_conversion", x => x.id);
                    table.ForeignKey(
                        name: "FK_unit_conversion_material_material_id",
                        column: x => x.material_id,
                        principalSchema: "dbo",
                        principalTable: "material",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "packer_payment",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    packer_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    payment_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    total_bags_packed = table.Column<int>(type: "int", nullable: false),
                    amount_paid = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    payment_completed = table.Column<bool>(type: "bit", maxLength: 20, nullable: false),
                    last_packing_paid = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    created_by = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_by = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    updated_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_packer_payment", x => x.id);
                    table.ForeignKey(
                        name: "FK_packer_payment_packer_packer_id",
                        column: x => x.packer_id,
                        principalSchema: "dbo",
                        principalTable: "packer",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "roles_claims",
                schema: "Security",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles_claims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_roles_claims_roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "Security",
                        principalTable: "roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "material_purchase",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    material_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    supplier_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    purchase_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    unit_price = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    quantity_purchased = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    balance = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    payment_status = table.Column<bool>(type: "bit", nullable: false),
                    measure_unit = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    created_by = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_by = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    updated_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_material_purchase", x => x.id);
                    table.ForeignKey(
                        name: "FK_material_purchase_material_material_id",
                        column: x => x.material_id,
                        principalSchema: "dbo",
                        principalTable: "material",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_material_purchase_supplier_supplier_id",
                        column: x => x.supplier_id,
                        principalSchema: "dbo",
                        principalTable: "supplier",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "supplier_payment",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    supplier_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    payment_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    amount_paid = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    payment_method = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    balance_remaining = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    created_by = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_by = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    updated_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_supplier_payment", x => x.id);
                    table.ForeignKey(
                        name: "FK_supplier_payment_supplier_supplier_id",
                        column: x => x.supplier_id,
                        principalSchema: "dbo",
                        principalTable: "supplier",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "customer",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    identity_number = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    first_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    last_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    phone_number = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    balance = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    is_active = table.Column<bool>(type: "bit", nullable: false),
                    created_by = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_by = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    updated_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customer", x => x.id);
                    table.ForeignKey(
                        name: "FK_customer_users_created_by",
                        column: x => x.created_by,
                        principalSchema: "Security",
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_customer_users_updated_by",
                        column: x => x.updated_by,
                        principalSchema: "Security",
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "packing",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    packing_number = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    packing_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    is_completed = table.Column<bool>(type: "bit", nullable: false),
                    PackerEntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    created_by = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_by = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    updated_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_packing", x => x.id);
                    table.ForeignKey(
                        name: "FK_packing_packer_PackerEntityId",
                        column: x => x.PackerEntityId,
                        principalSchema: "dbo",
                        principalTable: "packer",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_packing_users_created_by",
                        column: x => x.created_by,
                        principalSchema: "Security",
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_packing_users_updated_by",
                        column: x => x.updated_by,
                        principalSchema: "Security",
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "production",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    production_number = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    production_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    is_completed = table.Column<bool>(type: "bit", nullable: false),
                    created_by = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_by = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    updated_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_production", x => x.id);
                    table.ForeignKey(
                        name: "FK_production_users_created_by",
                        column: x => x.created_by,
                        principalSchema: "Security",
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_production_users_updated_by",
                        column: x => x.updated_by,
                        principalSchema: "Security",
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "user_claims",
                schema: "Security",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_claims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_user_claims_users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Security",
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "user_roles",
                schema: "Security",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_roles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_user_roles_roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "Security",
                        principalTable: "roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_user_roles_users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Security",
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "users_logins",
                schema: "Security",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users_logins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_users_logins_users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Security",
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "users_tokens",
                schema: "Security",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users_tokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_users_tokens_users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Security",
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "warehouse_control",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    control_number = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    control_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    observations = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    is_completed = table.Column<bool>(type: "bit", nullable: false),
                    last_closing_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    created_by = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_by = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    updated_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_warehouse_control", x => x.id);
                    table.ForeignKey(
                        name: "FK_warehouse_control_users_created_by",
                        column: x => x.created_by,
                        principalSchema: "Security",
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_warehouse_control_users_updated_by",
                        column: x => x.updated_by,
                        principalSchema: "Security",
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "warehouse_movement",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    movement_number = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MovementDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    created_by = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_by = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    updated_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_warehouse_movement", x => x.id);
                    table.ForeignKey(
                        name: "FK_warehouse_movement_users_created_by",
                        column: x => x.created_by,
                        principalSchema: "Security",
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_warehouse_movement_users_updated_by",
                        column: x => x.updated_by,
                        principalSchema: "Security",
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "customer_assistant",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    customer_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    assistant_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    created_by = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_by = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    updated_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customer_assistant", x => x.id);
                    table.ForeignKey(
                        name: "FK_customer_assistant_customer_customer_id",
                        column: x => x.customer_id,
                        principalSchema: "dbo",
                        principalTable: "customer",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "packing_detail",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    packing_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    production_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    bread_class_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    total_packed = table.Column<int>(type: "int", nullable: false),
                    damaged_in_packing = table.Column<int>(type: "int", nullable: false),
                    description_damaged = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    difference = table.Column<int>(type: "int", nullable: false),
                    created_by = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_by = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    updated_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_packing_detail", x => x.id);
                    table.ForeignKey(
                        name: "FK_packing_detail_bread_class_bread_class_id",
                        column: x => x.bread_class_id,
                        principalSchema: "dbo",
                        principalTable: "bread_class",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_packing_detail_packing_packing_id",
                        column: x => x.packing_id,
                        principalSchema: "dbo",
                        principalTable: "packing",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_packing_detail_production_production_id",
                        column: x => x.production_id,
                        principalSchema: "dbo",
                        principalTable: "production",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "production_detail",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    production_id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 50, nullable: false),
                    baker_id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 50, nullable: false),
                    bread_class_id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 50, nullable: false),
                    quantity_arrobas = table.Column<int>(type: "int", nullable: false),
                    quantity_latas = table.Column<int>(type: "int", nullable: false),
                    difference = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    created_by = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_by = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    updated_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_production_detail", x => x.id);
                    table.ForeignKey(
                        name: "FK_production_detail_baker_baker_id",
                        column: x => x.baker_id,
                        principalSchema: "dbo",
                        principalTable: "baker",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_production_detail_bread_class_bread_class_id",
                        column: x => x.bread_class_id,
                        principalSchema: "dbo",
                        principalTable: "bread_class",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_production_detail_production_production_id",
                        column: x => x.production_id,
                        principalSchema: "dbo",
                        principalTable: "production",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "order",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    order_number = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    order_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    customer_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    assistant_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    total_amount = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    outstanding_balance = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    order_type = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    is_paid = table.Column<bool>(type: "bit", nullable: false),
                    parent_order_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    created_by = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_by = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    updated_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order", x => x.id);
                    table.ForeignKey(
                        name: "FK_order_customer_assistant_assistant_id",
                        column: x => x.assistant_id,
                        principalSchema: "dbo",
                        principalTable: "customer_assistant",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_order_customer_customer_id",
                        column: x => x.customer_id,
                        principalSchema: "dbo",
                        principalTable: "customer",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_order_order_parent_order_id",
                        column: x => x.parent_order_id,
                        principalSchema: "dbo",
                        principalTable: "order",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_order_users_created_by",
                        column: x => x.created_by,
                        principalSchema: "Security",
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_order_users_updated_by",
                        column: x => x.updated_by,
                        principalSchema: "Security",
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "packing_packer",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    packing_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    packer_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PackingDetailEntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    created_by = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_by = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    updated_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_packing_packer", x => x.id);
                    table.ForeignKey(
                        name: "FK_packing_packer_packer_packer_id",
                        column: x => x.packer_id,
                        principalSchema: "dbo",
                        principalTable: "packer",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_packing_packer_packing_detail_PackingDetailEntityId",
                        column: x => x.PackingDetailEntityId,
                        principalSchema: "dbo",
                        principalTable: "packing_detail",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_packing_packer_packing_packing_id",
                        column: x => x.packing_id,
                        principalSchema: "dbo",
                        principalTable: "packing",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "order_detail",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    order_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    bread_class_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    unit_price = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    created_by = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_by = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    updated_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order_detail", x => x.id);
                    table.ForeignKey(
                        name: "FK_order_detail_bread_class_bread_class_id",
                        column: x => x.bread_class_id,
                        principalSchema: "dbo",
                        principalTable: "bread_class",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_order_detail_order_order_id",
                        column: x => x.order_id,
                        principalSchema: "dbo",
                        principalTable: "order",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "warehouse_control_detail",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    warehouse_control_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    bread_class_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    initial_stock = table.Column<int>(type: "int", nullable: false),
                    incoming_stock = table.Column<int>(type: "int", nullable: false),
                    packing_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    order_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    order_type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    adjustments = table.Column<int>(type: "int", nullable: false),
                    outgoing_stock = table.Column<int>(type: "int", nullable: false),
                    damaged_stock = table.Column<int>(type: "int", nullable: false),
                    final_stock = table.Column<int>(type: "int", nullable: false),
                    real_stock = table.Column<int>(type: "int", nullable: false),
                    difference = table.Column<int>(type: "int", nullable: false),
                    shortage = table.Column<int>(type: "int", nullable: true),
                    excess = table.Column<int>(type: "int", nullable: true),
                    created_by = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_by = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    updated_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_warehouse_control_detail", x => x.id);
                    table.ForeignKey(
                        name: "FK_warehouse_control_detail_bread_class_bread_class_id",
                        column: x => x.bread_class_id,
                        principalSchema: "dbo",
                        principalTable: "bread_class",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_warehouse_control_detail_order_order_id",
                        column: x => x.order_id,
                        principalSchema: "dbo",
                        principalTable: "order",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_warehouse_control_detail_packing_packing_id",
                        column: x => x.packing_id,
                        principalSchema: "dbo",
                        principalTable: "packing",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_warehouse_control_detail_warehouse_control_warehouse_control_id",
                        column: x => x.warehouse_control_id,
                        principalSchema: "dbo",
                        principalTable: "warehouse_control",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "warehouse_movement_detail",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    warehouse_movement_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    warehouse_control_detail_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    bread_class_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    movement_type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    observations = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    created_by = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_by = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    updated_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_warehouse_movement_detail", x => x.id);
                    table.ForeignKey(
                        name: "FK_warehouse_movement_detail_bread_class_bread_class_id",
                        column: x => x.bread_class_id,
                        principalSchema: "dbo",
                        principalTable: "bread_class",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_warehouse_movement_detail_warehouse_control_detail_warehouse_control_detail_id",
                        column: x => x.warehouse_control_detail_id,
                        principalSchema: "dbo",
                        principalTable: "warehouse_control_detail",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_warehouse_movement_detail_warehouse_movement_warehouse_movement_id",
                        column: x => x.warehouse_movement_id,
                        principalSchema: "dbo",
                        principalTable: "warehouse_movement",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_baker_payment_baker_id",
                schema: "dbo",
                table: "baker_payment",
                column: "baker_id");

            migrationBuilder.CreateIndex(
                name: "IX_bread_class_materials_bread_class_id",
                schema: "dbo",
                table: "bread_class_materials",
                column: "bread_class_id");

            migrationBuilder.CreateIndex(
                name: "IX_bread_class_materials_material_id",
                schema: "dbo",
                table: "bread_class_materials",
                column: "material_id");

            migrationBuilder.CreateIndex(
                name: "IX_customer_created_by",
                schema: "dbo",
                table: "customer",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_customer_updated_by",
                schema: "dbo",
                table: "customer",
                column: "updated_by");

            migrationBuilder.CreateIndex(
                name: "IX_customer_assistant_customer_id",
                schema: "dbo",
                table: "customer_assistant",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_material_purchase_material_id",
                schema: "dbo",
                table: "material_purchase",
                column: "material_id");

            migrationBuilder.CreateIndex(
                name: "IX_material_purchase_supplier_id",
                schema: "dbo",
                table: "material_purchase",
                column: "supplier_id");

            migrationBuilder.CreateIndex(
                name: "IX_order_assistant_id",
                schema: "dbo",
                table: "order",
                column: "assistant_id");

            migrationBuilder.CreateIndex(
                name: "IX_order_created_by",
                schema: "dbo",
                table: "order",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_order_customer_id",
                schema: "dbo",
                table: "order",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_order_parent_order_id",
                schema: "dbo",
                table: "order",
                column: "parent_order_id");

            migrationBuilder.CreateIndex(
                name: "IX_order_updated_by",
                schema: "dbo",
                table: "order",
                column: "updated_by");

            migrationBuilder.CreateIndex(
                name: "IX_order_detail_bread_class_id",
                schema: "dbo",
                table: "order_detail",
                column: "bread_class_id");

            migrationBuilder.CreateIndex(
                name: "IX_order_detail_order_id",
                schema: "dbo",
                table: "order_detail",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "IX_packer_payment_packer_id",
                schema: "dbo",
                table: "packer_payment",
                column: "packer_id");

            migrationBuilder.CreateIndex(
                name: "IX_packing_created_by",
                schema: "dbo",
                table: "packing",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_packing_PackerEntityId",
                schema: "dbo",
                table: "packing",
                column: "PackerEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_packing_updated_by",
                schema: "dbo",
                table: "packing",
                column: "updated_by");

            migrationBuilder.CreateIndex(
                name: "IX_packing_detail_bread_class_id",
                schema: "dbo",
                table: "packing_detail",
                column: "bread_class_id");

            migrationBuilder.CreateIndex(
                name: "IX_packing_detail_packing_id",
                schema: "dbo",
                table: "packing_detail",
                column: "packing_id");

            migrationBuilder.CreateIndex(
                name: "IX_packing_detail_production_id",
                schema: "dbo",
                table: "packing_detail",
                column: "production_id");

            migrationBuilder.CreateIndex(
                name: "IX_packing_packer_packer_id",
                schema: "dbo",
                table: "packing_packer",
                column: "packer_id");

            migrationBuilder.CreateIndex(
                name: "IX_packing_packer_packing_id",
                schema: "dbo",
                table: "packing_packer",
                column: "packing_id");

            migrationBuilder.CreateIndex(
                name: "IX_packing_packer_PackingDetailEntityId",
                schema: "dbo",
                table: "packing_packer",
                column: "PackingDetailEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_production_created_by",
                schema: "dbo",
                table: "production",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_production_updated_by",
                schema: "dbo",
                table: "production",
                column: "updated_by");

            migrationBuilder.CreateIndex(
                name: "IX_production_detail_baker_id",
                schema: "dbo",
                table: "production_detail",
                column: "baker_id");

            migrationBuilder.CreateIndex(
                name: "IX_production_detail_bread_class_id",
                schema: "dbo",
                table: "production_detail",
                column: "bread_class_id");

            migrationBuilder.CreateIndex(
                name: "IX_production_detail_production_id",
                schema: "dbo",
                table: "production_detail",
                column: "production_id");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "Security",
                table: "roles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_roles_claims_RoleId",
                schema: "Security",
                table: "roles_claims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_supplier_payment_supplier_id",
                schema: "dbo",
                table: "supplier_payment",
                column: "supplier_id");

            migrationBuilder.CreateIndex(
                name: "IX_unit_conversion_material_id",
                schema: "dbo",
                table: "unit_conversion",
                column: "material_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_claims_UserId",
                schema: "Security",
                table: "user_claims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_user_roles_RoleId",
                schema: "Security",
                table: "user_roles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "Security",
                table: "users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "Security",
                table: "users",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_users_logins_UserId",
                schema: "Security",
                table: "users_logins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_warehouse_control_created_by",
                schema: "dbo",
                table: "warehouse_control",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_warehouse_control_updated_by",
                schema: "dbo",
                table: "warehouse_control",
                column: "updated_by");

            migrationBuilder.CreateIndex(
                name: "IX_warehouse_control_detail_bread_class_id",
                schema: "dbo",
                table: "warehouse_control_detail",
                column: "bread_class_id");

            migrationBuilder.CreateIndex(
                name: "IX_warehouse_control_detail_order_id",
                schema: "dbo",
                table: "warehouse_control_detail",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "IX_warehouse_control_detail_packing_id",
                schema: "dbo",
                table: "warehouse_control_detail",
                column: "packing_id");

            migrationBuilder.CreateIndex(
                name: "IX_warehouse_control_detail_warehouse_control_id",
                schema: "dbo",
                table: "warehouse_control_detail",
                column: "warehouse_control_id");

            migrationBuilder.CreateIndex(
                name: "IX_warehouse_movement_created_by",
                schema: "dbo",
                table: "warehouse_movement",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_warehouse_movement_updated_by",
                schema: "dbo",
                table: "warehouse_movement",
                column: "updated_by");

            migrationBuilder.CreateIndex(
                name: "IX_warehouse_movement_detail_bread_class_id",
                schema: "dbo",
                table: "warehouse_movement_detail",
                column: "bread_class_id");

            migrationBuilder.CreateIndex(
                name: "IX_warehouse_movement_detail_warehouse_control_detail_id",
                schema: "dbo",
                table: "warehouse_movement_detail",
                column: "warehouse_control_detail_id");

            migrationBuilder.CreateIndex(
                name: "IX_warehouse_movement_detail_warehouse_movement_id",
                schema: "dbo",
                table: "warehouse_movement_detail",
                column: "warehouse_movement_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "baker_payment",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "bread_class_materials",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "material_purchase",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "order_detail",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "packer_payment",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "packing_packer",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "production_detail",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "roles_claims",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "supplier_payment",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "unit_conversion",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "user_claims",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "user_roles",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "users_logins",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "users_tokens",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "warehouse_movement_detail",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "packing_detail",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "baker",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "supplier",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "material",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "roles",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "warehouse_control_detail",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "warehouse_movement",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "production",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "bread_class",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "order",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "packing",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "warehouse_control",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "customer_assistant",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "packer",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "customer",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "users",
                schema: "Security");
        }
    }
}
