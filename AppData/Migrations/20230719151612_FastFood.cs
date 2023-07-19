using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppData.Migrations
{
    public partial class FastFood : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "drinks",
                columns: table => new
                {
                    IDDrink = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameDrink = table.Column<string>(type: "nvarchar(225)", maxLength: 225, nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Mass = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_drinks", x => x.IDDrink);
                });

            migrationBuilder.CreateTable(
                name: "mainDishes",
                columns: table => new
                {
                    IDMainDishes = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameMainDishes = table.Column<string>(type: "nvarchar(225)", maxLength: 225, nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Mass = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DescriptionMainDishes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mainDishes", x => x.IDMainDishes);
                });

            migrationBuilder.CreateTable(
                name: "payments",
                columns: table => new
                {
                    IDPayment = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Payment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bankaccount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankAccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageQR = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_payments", x => x.IDPayment);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    IDRole = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.IDRole);
                });

            migrationBuilder.CreateTable(
                name: "sideDishes",
                columns: table => new
                {
                    IDSideDishes = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameSideDishes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Mass = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DescriptionSideDishes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sideDishes", x => x.IDSideDishes);
                });

            migrationBuilder.CreateTable(
                name: "vouchers",
                columns: table => new
                {
                    IDVoucher = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VoucherCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quatity = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Condition = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vouchers", x => x.IDVoucher);
                });

            migrationBuilder.CreateTable(
                name: "customers",
                columns: table => new
                {
                    IDCustomer = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IDRole = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameCustomer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PassWord = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    District = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customers", x => x.IDCustomer);
                    table.ForeignKey(
                        name: "FK_customers_roles_IDRole",
                        column: x => x.IDRole,
                        principalTable: "roles",
                        principalColumn: "IDRole",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "comboFastFoods",
                columns: table => new
                {
                    IDCombo = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IDSideDishes = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IDMainDishes = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IDDrink = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    NameCombo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PriceCombo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DescriptionCombo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_comboFastFoods", x => x.IDCombo);
                    table.ForeignKey(
                        name: "FK_comboFastFoods_drinks_IDDrink",
                        column: x => x.IDDrink,
                        principalTable: "drinks",
                        principalColumn: "IDDrink");
                    table.ForeignKey(
                        name: "FK_comboFastFoods_mainDishes_IDMainDishes",
                        column: x => x.IDMainDishes,
                        principalTable: "mainDishes",
                        principalColumn: "IDMainDishes");
                    table.ForeignKey(
                        name: "FK_comboFastFoods_sideDishes_IDSideDishes",
                        column: x => x.IDSideDishes,
                        principalTable: "sideDishes",
                        principalColumn: "IDSideDishes");
                });

            migrationBuilder.CreateTable(
                name: "bills",
                columns: table => new
                {
                    IDBill = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IDVoucher = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IDCustomer = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IDPayment = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InvoiceCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quatity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameReceiver = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneReceiver = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CityReceiver = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DistrictReceiver = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransportFee = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalPayment = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateOfPayment = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bills", x => x.IDBill);
                    table.ForeignKey(
                        name: "FK_bills_customers_IDCustomer",
                        column: x => x.IDCustomer,
                        principalTable: "customers",
                        principalColumn: "IDCustomer",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_bills_payments_IDPayment",
                        column: x => x.IDPayment,
                        principalTable: "payments",
                        principalColumn: "IDPayment",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_bills_vouchers_IDVoucher",
                        column: x => x.IDVoucher,
                        principalTable: "vouchers",
                        principalColumn: "IDVoucher",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "carts",
                columns: table => new
                {
                    IDCart = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_carts", x => x.IDCart);
                    table.ForeignKey(
                        name: "FK_carts_customers_IDCart",
                        column: x => x.IDCart,
                        principalTable: "customers",
                        principalColumn: "IDCustomer",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "setkey",
                columns: table => new
                {
                    IDSetKey = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IDMain = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IDCombo = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IDSide = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IDDrink = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_setkey", x => x.IDSetKey);
                    table.ForeignKey(
                        name: "FK_setkey_comboFastFoods_IDCombo",
                        column: x => x.IDCombo,
                        principalTable: "comboFastFoods",
                        principalColumn: "IDCombo");
                    table.ForeignKey(
                        name: "FK_setkey_drinks_IDDrink",
                        column: x => x.IDDrink,
                        principalTable: "drinks",
                        principalColumn: "IDDrink");
                    table.ForeignKey(
                        name: "FK_setkey_mainDishes_IDMain",
                        column: x => x.IDMain,
                        principalTable: "mainDishes",
                        principalColumn: "IDMainDishes");
                    table.ForeignKey(
                        name: "FK_setkey_sideDishes_IDSide",
                        column: x => x.IDSide,
                        principalTable: "sideDishes",
                        principalColumn: "IDSideDishes");
                });

            migrationBuilder.CreateTable(
                name: "billDetails",
                columns: table => new
                {
                    IDBillDetail = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IDBill = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IDFood = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameFood = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quatity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BillIDBill = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ComboFastFoodIDCombo = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DrinksIDDrink = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MainDishesIDMainDishes = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SideDishesIDSideDishes = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_billDetails", x => x.IDBillDetail);
                    table.ForeignKey(
                        name: "FK_billDetails_bills_BillIDBill",
                        column: x => x.BillIDBill,
                        principalTable: "bills",
                        principalColumn: "IDBill");
                    table.ForeignKey(
                        name: "FK_billDetails_comboFastFoods_ComboFastFoodIDCombo",
                        column: x => x.ComboFastFoodIDCombo,
                        principalTable: "comboFastFoods",
                        principalColumn: "IDCombo");
                    table.ForeignKey(
                        name: "FK_billDetails_drinks_DrinksIDDrink",
                        column: x => x.DrinksIDDrink,
                        principalTable: "drinks",
                        principalColumn: "IDDrink");
                    table.ForeignKey(
                        name: "FK_billDetails_mainDishes_MainDishesIDMainDishes",
                        column: x => x.MainDishesIDMainDishes,
                        principalTable: "mainDishes",
                        principalColumn: "IDMainDishes");
                    table.ForeignKey(
                        name: "FK_billDetails_sideDishes_SideDishesIDSideDishes",
                        column: x => x.SideDishesIDSideDishes,
                        principalTable: "sideDishes",
                        principalColumn: "IDSideDishes");
                });

            migrationBuilder.CreateTable(
                name: "cartDetails",
                columns: table => new
                {
                    IDCartDetail = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IDCart = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IDsetkey = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IDFood = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameFood = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quatity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ComboFastFoodIDCombo = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DrinksIDDrink = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MainDishesIDMainDishes = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SideDishesIDSideDishes = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cartDetails", x => x.IDCartDetail);
                    table.ForeignKey(
                        name: "FK_cartDetails_carts_IDCart",
                        column: x => x.IDCart,
                        principalTable: "carts",
                        principalColumn: "IDCart",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_cartDetails_comboFastFoods_ComboFastFoodIDCombo",
                        column: x => x.ComboFastFoodIDCombo,
                        principalTable: "comboFastFoods",
                        principalColumn: "IDCombo");
                    table.ForeignKey(
                        name: "FK_cartDetails_drinks_DrinksIDDrink",
                        column: x => x.DrinksIDDrink,
                        principalTable: "drinks",
                        principalColumn: "IDDrink");
                    table.ForeignKey(
                        name: "FK_cartDetails_mainDishes_MainDishesIDMainDishes",
                        column: x => x.MainDishesIDMainDishes,
                        principalTable: "mainDishes",
                        principalColumn: "IDMainDishes");
                    table.ForeignKey(
                        name: "FK_cartDetails_setkey_IDsetkey",
                        column: x => x.IDsetkey,
                        principalTable: "setkey",
                        principalColumn: "IDSetKey",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_cartDetails_sideDishes_SideDishesIDSideDishes",
                        column: x => x.SideDishesIDSideDishes,
                        principalTable: "sideDishes",
                        principalColumn: "IDSideDishes");
                });

            migrationBuilder.CreateIndex(
                name: "IX_billDetails_BillIDBill",
                table: "billDetails",
                column: "BillIDBill");

            migrationBuilder.CreateIndex(
                name: "IX_billDetails_ComboFastFoodIDCombo",
                table: "billDetails",
                column: "ComboFastFoodIDCombo");

            migrationBuilder.CreateIndex(
                name: "IX_billDetails_DrinksIDDrink",
                table: "billDetails",
                column: "DrinksIDDrink");

            migrationBuilder.CreateIndex(
                name: "IX_billDetails_MainDishesIDMainDishes",
                table: "billDetails",
                column: "MainDishesIDMainDishes");

            migrationBuilder.CreateIndex(
                name: "IX_billDetails_SideDishesIDSideDishes",
                table: "billDetails",
                column: "SideDishesIDSideDishes");

            migrationBuilder.CreateIndex(
                name: "IX_bills_IDCustomer",
                table: "bills",
                column: "IDCustomer");

            migrationBuilder.CreateIndex(
                name: "IX_bills_IDPayment",
                table: "bills",
                column: "IDPayment");

            migrationBuilder.CreateIndex(
                name: "IX_bills_IDVoucher",
                table: "bills",
                column: "IDVoucher");

            migrationBuilder.CreateIndex(
                name: "IX_cartDetails_ComboFastFoodIDCombo",
                table: "cartDetails",
                column: "ComboFastFoodIDCombo");

            migrationBuilder.CreateIndex(
                name: "IX_cartDetails_DrinksIDDrink",
                table: "cartDetails",
                column: "DrinksIDDrink");

            migrationBuilder.CreateIndex(
                name: "IX_cartDetails_IDCart",
                table: "cartDetails",
                column: "IDCart");

            migrationBuilder.CreateIndex(
                name: "IX_cartDetails_IDsetkey",
                table: "cartDetails",
                column: "IDsetkey");

            migrationBuilder.CreateIndex(
                name: "IX_cartDetails_MainDishesIDMainDishes",
                table: "cartDetails",
                column: "MainDishesIDMainDishes");

            migrationBuilder.CreateIndex(
                name: "IX_cartDetails_SideDishesIDSideDishes",
                table: "cartDetails",
                column: "SideDishesIDSideDishes");

            migrationBuilder.CreateIndex(
                name: "IX_comboFastFoods_IDDrink",
                table: "comboFastFoods",
                column: "IDDrink");

            migrationBuilder.CreateIndex(
                name: "IX_comboFastFoods_IDMainDishes",
                table: "comboFastFoods",
                column: "IDMainDishes");

            migrationBuilder.CreateIndex(
                name: "IX_comboFastFoods_IDSideDishes",
                table: "comboFastFoods",
                column: "IDSideDishes");

            migrationBuilder.CreateIndex(
                name: "IX_customers_IDRole",
                table: "customers",
                column: "IDRole");

            migrationBuilder.CreateIndex(
                name: "IX_setkey_IDCombo",
                table: "setkey",
                column: "IDCombo");

            migrationBuilder.CreateIndex(
                name: "IX_setkey_IDDrink",
                table: "setkey",
                column: "IDDrink");

            migrationBuilder.CreateIndex(
                name: "IX_setkey_IDMain",
                table: "setkey",
                column: "IDMain");

            migrationBuilder.CreateIndex(
                name: "IX_setkey_IDSide",
                table: "setkey",
                column: "IDSide");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "billDetails");

            migrationBuilder.DropTable(
                name: "cartDetails");

            migrationBuilder.DropTable(
                name: "bills");

            migrationBuilder.DropTable(
                name: "carts");

            migrationBuilder.DropTable(
                name: "setkey");

            migrationBuilder.DropTable(
                name: "payments");

            migrationBuilder.DropTable(
                name: "vouchers");

            migrationBuilder.DropTable(
                name: "customers");

            migrationBuilder.DropTable(
                name: "comboFastFoods");

            migrationBuilder.DropTable(
                name: "roles");

            migrationBuilder.DropTable(
                name: "drinks");

            migrationBuilder.DropTable(
                name: "mainDishes");

            migrationBuilder.DropTable(
                name: "sideDishes");
        }
    }
}
