using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace K22CNT4_PhamThiThuHuyen_2210900030_DATN.Migrations
{
    /// <inheritdoc />
    public partial class AddTransportMethodToOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ADMINS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Role = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, defaultValue: "Admin"),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    Status = table.Column<bool>(type: "bit", nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ADMINS__3214EC274C042050", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CATEGORY",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NAME = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    ICON = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    PARENTID = table.Column<long>(type: "bigint", nullable: true),
                    SLUG = table.Column<string>(type: "nvarchar(160)", maxLength: 160, nullable: true),
                    META_TITLE = table.Column<string>(type: "nvarchar(160)", maxLength: 160, nullable: true),
                    META_KEYWORD = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    META_DESC = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CREATED_DATE = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    ISDELETE = table.Column<byte>(type: "tinyint", nullable: true, defaultValue: (byte)0),
                    ISACTIVE = table.Column<byte>(type: "tinyint", nullable: true, defaultValue: (byte)1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CATEGORY__A50F9896210394B9", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CATEGORY_PARENT",
                        column: x => x.PARENTID,
                        principalTable: "CATEGORY",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "COLOR",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NAME = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CODE = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ISACTIVE = table.Column<byte>(type: "tinyint", nullable: true, defaultValue: (byte)1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__COLOR__6FDDF3C465CC4824", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CONTACT",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FULLNAME = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    EMAIL = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    PHONE = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SUBJECT = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    MESSAGE = table.Column<string>(type: "ntext", nullable: false),
                    CREATED_DATE = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    IS_READ = table.Column<byte>(type: "tinyint", nullable: true, defaultValue: (byte)0),
                    REPLY = table.Column<string>(type: "ntext", nullable: true),
                    REPLY_DATE = table.Column<DateTime>(type: "datetime", nullable: true),
                    ISDELETE = table.Column<byte>(type: "tinyint", nullable: true, defaultValue: (byte)0),
                    ISACTIVE = table.Column<byte>(type: "tinyint", nullable: true, defaultValue: (byte)1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CONTACT__79911868EA517AC6", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CUSTOMER",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    USERNAME = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PASSWORD = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    EMAIL = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    PHONE = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ADDRESS = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    AVATAR = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    USER_TYPE = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, defaultValue: "CUSTOMER"),
                    CREATED_DATE = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    ISDELETE = table.Column<byte>(type: "tinyint", nullable: true, defaultValue: (byte)0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CUSTOMER__61DBD7885F7E911F", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "MATERIAL",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NAME = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    NOTES = table.Column<string>(type: "ntext", nullable: true),
                    ISACTIVE = table.Column<byte>(type: "tinyint", nullable: true, defaultValue: (byte)1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__MATERIAL__278B51D527E482C7", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PAY_METHOD",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NAME = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    NOTES = table.Column<string>(type: "ntext", nullable: true),
                    CREATED_DATE = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    ISDELETE = table.Column<byte>(type: "tinyint", nullable: true, defaultValue: (byte)0),
                    ISACTIVE = table.Column<byte>(type: "tinyint", nullable: true, defaultValue: (byte)1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PAY_METH__55A067C94A097C18", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SIZE",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NAME = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AGE_FROM = table.Column<int>(type: "int", nullable: true),
                    AGE_TO = table.Column<int>(type: "int", nullable: true),
                    ISACTIVE = table.Column<byte>(type: "tinyint", nullable: true, defaultValue: (byte)1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__SIZE__903CA348CE388A5F", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TRANSPORT_METHOD",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NAME = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    NOTES = table.Column<string>(type: "ntext", nullable: true),
                    CREATED_DATE = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    ISDELETE = table.Column<byte>(type: "tinyint", nullable: true, defaultValue: (byte)0),
                    ISACTIVE = table.Column<byte>(type: "tinyint", nullable: true, defaultValue: (byte)1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TRANSPOR__15CD30836FAFC2D8", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PRODUCT",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NAME = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    DESCRIPTION = table.Column<string>(type: "ntext", nullable: true),
                    CONTENTS = table.Column<string>(type: "ntext", nullable: true),
                    CATEGORYID = table.Column<long>(type: "bigint", nullable: true),
                    PRICE = table.Column<decimal>(type: "decimal(18,2)", nullable: true, defaultValue: 0m),
                    GENDER = table.Column<int>(type: "int", nullable: false),
                    SLUG = table.Column<string>(type: "nvarchar(160)", maxLength: 160, nullable: true),
                    META_TITLE = table.Column<string>(type: "nvarchar(160)", maxLength: 160, nullable: true),
                    META_KEYWORD = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    META_DESC = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CREATED_DATE = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    ISDELETE = table.Column<byte>(type: "tinyint", nullable: true, defaultValue: (byte)0),
                    ISACTIVE = table.Column<byte>(type: "tinyint", nullable: true, defaultValue: (byte)1),
                    ISSALE = table.Column<byte>(type: "tinyint", nullable: true),
                    ISTOPSALE = table.Column<byte>(type: "tinyint", nullable: true),
                    ISHOME = table.Column<byte>(type: "tinyint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PRODUCT__34980AA2D4885042", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PRODUCT_CATEGORY",
                        column: x => x.CATEGORYID,
                        principalTable: "CATEGORY",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "ORDERS",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ORDERS_DATE = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CUSTOMERID = table.Column<long>(type: "bigint", nullable: true),
                    TOTAL_MONEY = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    NAME_RECEIVER = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ADDRESS = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    PHONE = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ISDELETE = table.Column<byte>(type: "tinyint", nullable: true, defaultValue: (byte)0),
                    ISACTIVE = table.Column<byte>(type: "tinyint", nullable: true, defaultValue: (byte)1),
                    TransportMethodid = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ORDERS__B0B1A3EBEB50343D", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ORDERS_CUSTOMER",
                        column: x => x.CUSTOMERID,
                        principalTable: "CUSTOMER",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_ORDERS_TRANSPORT_METHOD_TransportMethodid",
                        column: x => x.TransportMethodid,
                        principalTable: "TRANSPORT_METHOD",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "PRODUCT_IMAGES",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PRODUCTID = table.Column<long>(type: "bigint", nullable: false),
                    COLORID = table.Column<long>(type: "bigint", nullable: true),
                    URLIMG = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    ISDEFAULT = table.Column<byte>(type: "tinyint", nullable: true, defaultValue: (byte)0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PRODUCT___BE142DD3D08164A2", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PI_COLOR",
                        column: x => x.COLORID,
                        principalTable: "COLOR",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_PI_PRODUCT",
                        column: x => x.PRODUCTID,
                        principalTable: "PRODUCT",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "PRODUCT_VARIANT",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PRODUCTID = table.Column<long>(type: "bigint", nullable: false),
                    COLORID = table.Column<long>(type: "bigint", nullable: false),
                    SIZEID = table.Column<long>(type: "bigint", nullable: false),
                    MATERIALID = table.Column<long>(type: "bigint", nullable: false),
                    PRICE = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    QUANTITY = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    ISACTIVE = table.Column<byte>(type: "tinyint", nullable: true, defaultValue: (byte)1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PRODUCT___1342E27527D2FC8E", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PV_COLOR",
                        column: x => x.COLORID,
                        principalTable: "COLOR",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_PV_MATERIAL",
                        column: x => x.MATERIALID,
                        principalTable: "MATERIAL",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_PV_PRODUCT",
                        column: x => x.PRODUCTID,
                        principalTable: "PRODUCT",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_PV_SIZE",
                        column: x => x.SIZEID,
                        principalTable: "SIZE",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "REVIEW",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PRODUCTID = table.Column<long>(type: "bigint", nullable: false),
                    CUSTOMERID = table.Column<long>(type: "bigint", nullable: true),
                    FULLNAME = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    EMAIL = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    RATING = table.Column<byte>(type: "tinyint", nullable: false),
                    TITLE = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CONTENT = table.Column<string>(type: "ntext", nullable: false),
                    CREATED_DATE = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    IS_APPROVED = table.Column<byte>(type: "tinyint", nullable: true, defaultValue: (byte)0),
                    REPLY = table.Column<string>(type: "ntext", nullable: true),
                    REPLY_DATE = table.Column<DateTime>(type: "datetime", nullable: true),
                    ISDELETE = table.Column<byte>(type: "tinyint", nullable: true, defaultValue: (byte)0),
                    ISACTIVE = table.Column<byte>(type: "tinyint", nullable: true, defaultValue: (byte)1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__REVIEW__DDDCEB4A57C3AC9B", x => x.ID);
                    table.ForeignKey(
                        name: "FK_REVIEW_CUSTOMER",
                        column: x => x.CUSTOMERID,
                        principalTable: "CUSTOMER",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_REVIEW_PRODUCT",
                        column: x => x.PRODUCTID,
                        principalTable: "PRODUCT",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "ORDERS_DETAILS",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ORDERSID = table.Column<long>(type: "bigint", nullable: false),
                    PRODUCTVARIANTID = table.Column<long>(type: "bigint", nullable: false),
                    PRICE = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    QUANTITY = table.Column<int>(type: "int", nullable: true, defaultValue: 1),
                    TOTAL = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ORDERS_D__E795C3F15F283861", x => x.ID);
                    table.ForeignKey(
                        name: "FK_OD_ORDERS",
                        column: x => x.ORDERSID,
                        principalTable: "ORDERS",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_OD_VARIANT",
                        column: x => x.PRODUCTVARIANTID,
                        principalTable: "PRODUCT_VARIANT",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CATEGORY_PARENTID",
                table: "CATEGORY",
                column: "PARENTID");

            migrationBuilder.CreateIndex(
                name: "UQ__CUSTOMER__161CF7247B92D8CB",
                table: "CUSTOMER",
                column: "EMAIL",
                unique: true,
                filter: "[EMAIL] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UQ__CUSTOMER__B15BE12EF6D38386",
                table: "CUSTOMER",
                column: "USERNAME",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ORDERS_CUSTOMERID",
                table: "ORDERS",
                column: "CUSTOMERID");

            migrationBuilder.CreateIndex(
                name: "IX_ORDERS_TransportMethodid",
                table: "ORDERS",
                column: "TransportMethodid");

            migrationBuilder.CreateIndex(
                name: "IX_ORDERS_DETAILS_ORDERSID",
                table: "ORDERS_DETAILS",
                column: "ORDERSID");

            migrationBuilder.CreateIndex(
                name: "IX_ORDERS_DETAILS_PRODUCTVARIANTID",
                table: "ORDERS_DETAILS",
                column: "PRODUCTVARIANTID");

            migrationBuilder.CreateIndex(
                name: "IX_PRODUCT_CATEGORYID",
                table: "PRODUCT",
                column: "CATEGORYID");

            migrationBuilder.CreateIndex(
                name: "IX_PRODUCT_IMAGES_COLORID",
                table: "PRODUCT_IMAGES",
                column: "COLORID");

            migrationBuilder.CreateIndex(
                name: "IX_PRODUCT_IMAGES_PRODUCTID",
                table: "PRODUCT_IMAGES",
                column: "PRODUCTID");

            migrationBuilder.CreateIndex(
                name: "IX_PRODUCT_VARIANT_COLORID",
                table: "PRODUCT_VARIANT",
                column: "COLORID");

            migrationBuilder.CreateIndex(
                name: "IX_PRODUCT_VARIANT_MATERIALID",
                table: "PRODUCT_VARIANT",
                column: "MATERIALID");

            migrationBuilder.CreateIndex(
                name: "IX_PRODUCT_VARIANT_PRODUCTID",
                table: "PRODUCT_VARIANT",
                column: "PRODUCTID");

            migrationBuilder.CreateIndex(
                name: "IX_PRODUCT_VARIANT_SIZEID",
                table: "PRODUCT_VARIANT",
                column: "SIZEID");

            migrationBuilder.CreateIndex(
                name: "IX_REVIEW_CUSTOMERID",
                table: "REVIEW",
                column: "CUSTOMERID");

            migrationBuilder.CreateIndex(
                name: "IX_REVIEW_PRODUCTID",
                table: "REVIEW",
                column: "PRODUCTID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ADMINS");

            migrationBuilder.DropTable(
                name: "CONTACT");

            migrationBuilder.DropTable(
                name: "ORDERS_DETAILS");

            migrationBuilder.DropTable(
                name: "PAY_METHOD");

            migrationBuilder.DropTable(
                name: "PRODUCT_IMAGES");

            migrationBuilder.DropTable(
                name: "REVIEW");

            migrationBuilder.DropTable(
                name: "ORDERS");

            migrationBuilder.DropTable(
                name: "PRODUCT_VARIANT");

            migrationBuilder.DropTable(
                name: "CUSTOMER");

            migrationBuilder.DropTable(
                name: "TRANSPORT_METHOD");

            migrationBuilder.DropTable(
                name: "COLOR");

            migrationBuilder.DropTable(
                name: "MATERIAL");

            migrationBuilder.DropTable(
                name: "PRODUCT");

            migrationBuilder.DropTable(
                name: "SIZE");

            migrationBuilder.DropTable(
                name: "CATEGORY");
        }
    }
}
