using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace pmo.Migrations
{
    public partial class SeedDataUserCitizenship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "TagCategories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.InsertData(
                table: "TagCategories",
                columns: new[] { "Id", "CreateDate", "FriendlyName", "IsFixed", "Key", "ModifiedByUser" },
                values: new object[] { 2, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), "Citizenships", true, "citizenships", null });

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "CreateDate", "ModifiedByUser", "Name", "TagCategoryId" },
                values: new object[,]
                {
                    { 16, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Afghanistan", 2 },
                    { 142, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Nicaragua", 2 },
                    { 143, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Niger", 2 },
                    { 144, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Nigeria", 2 },
                    { 145, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "North Korea", 2 },
                    { 146, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "North Macedonia (formerly Macedonia)", 2 },
                    { 147, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Norway", 2 },
                    { 148, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Oman", 2 },
                    { 149, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Pakistan", 2 },
                    { 150, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Palau", 2 },
                    { 151, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Palestine", 2 },
                    { 152, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Panama", 2 },
                    { 153, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Papua New Guinea", 2 },
                    { 154, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Paraguay", 2 },
                    { 155, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Peru", 2 },
                    { 156, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Philippines", 2 },
                    { 157, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Poland", 2 },
                    { 158, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Portugal", 2 },
                    { 159, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Qatar", 2 },
                    { 160, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Romania", 2 },
                    { 161, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Russia", 2 },
                    { 162, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Rwanda", 2 },
                    { 141, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "New Zealand", 2 },
                    { 163, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Saint Kitts and Nevis", 2 },
                    { 140, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Netherlands", 2 },
                    { 138, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Nauru", 2 },
                    { 117, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Lithuania", 2 },
                    { 118, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Luxembourg", 2 },
                    { 119, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Madagascar", 2 },
                    { 120, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Malawi", 2 },
                    { 121, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Malaysia", 2 },
                    { 122, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Maldives", 2 },
                    { 123, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Mali", 2 },
                    { 124, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Malta", 2 },
                    { 125, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Marshall Islands", 2 },
                    { 126, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Mauritania", 2 },
                    { 127, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Mauritius", 2 },
                    { 128, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Mexico", 2 },
                    { 129, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Micronesia", 2 },
                    { 130, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Moldova", 2 },
                    { 131, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Monaco", 2 },
                    { 132, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Mongolia", 2 },
                    { 133, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Montenegro", 2 },
                    { 134, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Morocco", 2 },
                    { 135, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Mozambique", 2 },
                    { 136, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Myanmar (formerly Burma)", 2 },
                    { 137, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Namibia", 2 },
                    { 139, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Nepal", 2 },
                    { 116, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Liechtenstein", 2 },
                    { 164, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Saint Lucia", 2 },
                    { 166, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Samoa", 2 },
                    { 192, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Thailand", 2 },
                    { 193, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Timor-Leste", 2 },
                    { 194, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Togo", 2 },
                    { 195, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Tonga", 2 },
                    { 196, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Trinidad and Tobago", 2 },
                    { 197, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Tunisia", 2 },
                    { 198, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Turkey", 2 },
                    { 199, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Turkmenistan", 2 },
                    { 200, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Tuvalu", 2 },
                    { 201, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Uganda", 2 },
                    { 202, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Ukraine", 2 },
                    { 203, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "United Arab Emirates (UAE)", 2 },
                    { 204, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "United Kingdom (UK)", 2 },
                    { 205, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "United States of America (USA)", 2 },
                    { 206, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Uruguay", 2 },
                    { 207, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Uzbekistan", 2 },
                    { 208, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Vanuatu", 2 },
                    { 209, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Vatican City (Holy See)", 2 },
                    { 210, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Venezuela", 2 },
                    { 211, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Vietnam", 2 },
                    { 212, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Yemen", 2 },
                    { 191, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Tanzania", 2 },
                    { 165, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Saint Vincent and the Grenadines", 2 },
                    { 190, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Tajikistan", 2 },
                    { 188, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Syria", 2 },
                    { 167, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "San Marino", 2 },
                    { 168, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Sao Tome and Principe", 2 },
                    { 169, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Saudi Arabia", 2 },
                    { 170, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Senegal", 2 },
                    { 171, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Serbia", 2 },
                    { 172, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Seychelles", 2 },
                    { 173, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Sierra Leone", 2 },
                    { 174, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Singapore", 2 },
                    { 175, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Slovakia", 2 },
                    { 176, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Slovenia", 2 },
                    { 177, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Solomon Islands", 2 },
                    { 178, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Somalia", 2 },
                    { 179, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "South Africa", 2 },
                    { 180, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "South Korea", 2 },
                    { 181, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "South Sudan", 2 },
                    { 182, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Spain", 2 },
                    { 183, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Sri Lanka", 2 },
                    { 184, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Sudan", 2 },
                    { 185, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Suriname", 2 },
                    { 186, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Sweden", 2 },
                    { 187, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Switzerland", 2 },
                    { 189, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Taiwan", 2 },
                    { 213, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Zambia", 2 },
                    { 115, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Libya", 2 },
                    { 113, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Lesotho", 2 },
                    { 42, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Burkina Faso", 2 },
                    { 43, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Burundi", 2 },
                    { 44, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Cabo Verde", 2 },
                    { 45, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Cambodia", 2 },
                    { 46, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Cameroon", 2 },
                    { 47, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Canada", 2 },
                    { 48, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Central African Republic (CAR)", 2 },
                    { 49, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Chad", 2 },
                    { 50, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Chile", 2 },
                    { 51, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "China", 2 },
                    { 52, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Colombia", 2 },
                    { 53, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Comoros", 2 },
                    { 54, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Congo", 2 },
                    { 55, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Democratic Republic of the", 2 },
                    { 56, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Congo", 2 },
                    { 57, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Republic of the", 2 },
                    { 58, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Costa Rica", 2 },
                    { 59, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Cote d'Ivoire", 2 },
                    { 60, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Croatia", 2 },
                    { 61, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Cuba", 2 },
                    { 62, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Cyprus", 2 },
                    { 41, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Bulgaria", 2 },
                    { 63, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Czechia", 2 },
                    { 40, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Brunei", 2 },
                    { 38, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Botswana", 2 },
                    { 17, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Albania", 2 },
                    { 18, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Algeria", 2 },
                    { 19, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Andorra", 2 },
                    { 20, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Angola", 2 },
                    { 21, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Antigua and Barbuda", 2 },
                    { 22, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Argentina", 2 },
                    { 23, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Armenia", 2 },
                    { 24, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Australia", 2 },
                    { 25, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Austria", 2 },
                    { 26, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Azerbaijan", 2 },
                    { 27, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Bahamas", 2 },
                    { 28, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Bahrain", 2 },
                    { 29, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Bangladesh", 2 },
                    { 30, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Barbados", 2 },
                    { 31, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Belarus", 2 },
                    { 32, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Belgium", 2 },
                    { 33, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Belize", 2 },
                    { 34, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Benin", 2 },
                    { 35, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Bhutan", 2 },
                    { 36, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Bolivia", 2 },
                    { 37, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Bosnia and Herzegovina", 2 },
                    { 39, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Brazil", 2 },
                    { 114, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Liberia", 2 },
                    { 64, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Denmark", 2 },
                    { 66, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Dominica", 2 },
                    { 92, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Hungary", 2 },
                    { 93, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Iceland", 2 },
                    { 94, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "India", 2 },
                    { 95, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Indonesia", 2 },
                    { 96, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Iran", 2 },
                    { 97, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Iraq", 2 },
                    { 98, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Ireland", 2 },
                    { 99, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Israel", 2 },
                    { 100, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Italy", 2 },
                    { 101, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Jamaica", 2 },
                    { 102, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Japan", 2 },
                    { 103, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Jordan", 2 },
                    { 104, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Kazakhstan", 2 },
                    { 105, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Kenya", 2 },
                    { 106, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Kiribati", 2 },
                    { 107, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Kosovo", 2 },
                    { 108, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Kuwait", 2 },
                    { 109, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Kyrgyzstan", 2 },
                    { 110, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Laos", 2 },
                    { 111, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Latvia", 2 },
                    { 112, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Lebanon", 2 },
                    { 91, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Honduras", 2 },
                    { 65, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Djibouti", 2 },
                    { 90, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Haiti", 2 },
                    { 88, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Guinea-Bissau", 2 },
                    { 67, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Dominican Republic", 2 },
                    { 68, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Ecuador", 2 },
                    { 69, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Egypt", 2 },
                    { 70, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "El Salvador", 2 },
                    { 71, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Equatorial Guinea", 2 },
                    { 72, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Eritrea", 2 },
                    { 73, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Estonia", 2 },
                    { 74, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Eswatini (formerly Swaziland)", 2 },
                    { 75, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Ethiopia", 2 },
                    { 76, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Fiji", 2 },
                    { 77, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Finland", 2 },
                    { 78, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "France", 2 },
                    { 79, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Gabon", 2 },
                    { 80, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Gambia", 2 },
                    { 81, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Georgia", 2 },
                    { 82, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Germany", 2 },
                    { 83, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Ghana", 2 },
                    { 84, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Greece", 2 },
                    { 85, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Grenada", 2 },
                    { 86, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Guatemala", 2 },
                    { 87, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Guinea", 2 },
                    { 89, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Guyana", 2 },
                    { 214, new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307), null, "Zimbabwe", 2 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 61);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 62);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 63);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 64);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 65);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 66);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 67);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 68);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 69);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 70);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 71);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 72);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 73);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 74);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 75);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 76);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 77);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 78);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 79);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 80);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 81);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 82);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 83);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 84);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 85);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 86);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 87);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 88);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 89);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 90);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 91);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 92);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 93);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 94);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 95);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 96);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 97);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 98);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 99);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 100);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 102);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 103);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 104);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 105);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 106);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 107);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 108);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 109);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 110);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 111);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 112);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 113);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 114);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 115);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 116);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 117);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 118);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 119);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 120);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 121);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 122);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 123);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 124);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 125);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 126);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 127);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 128);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 129);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 130);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 131);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 132);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 133);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 134);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 135);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 136);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 137);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 138);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 139);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 140);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 141);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 142);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 143);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 144);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 145);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 146);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 147);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 148);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 149);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 150);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 151);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 152);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 153);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 154);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 155);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 156);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 157);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 158);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 159);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 160);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 161);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 162);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 163);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 164);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 165);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 166);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 167);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 168);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 169);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 170);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 171);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 172);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 173);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 174);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 175);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 176);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 177);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 178);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 179);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 180);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 181);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 182);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 183);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 184);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 185);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 186);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 187);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 188);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 189);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 190);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 191);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 192);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 193);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 194);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 195);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 196);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 197);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 198);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 199);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 200);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 201);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 202);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 203);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 204);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 205);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 206);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 207);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 208);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 209);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 210);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 211);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 212);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 213);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 214);

            migrationBuilder.DeleteData(
                table: "TagCategories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "TagCategories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 13, 51, 33, 452, DateTimeKind.Local).AddTicks(2005));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 13, 51, 33, 452, DateTimeKind.Local).AddTicks(2005));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 13, 51, 33, 452, DateTimeKind.Local).AddTicks(2005));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 13, 51, 33, 452, DateTimeKind.Local).AddTicks(2005));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 13, 51, 33, 452, DateTimeKind.Local).AddTicks(2005));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 13, 51, 33, 452, DateTimeKind.Local).AddTicks(2005));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 13, 51, 33, 452, DateTimeKind.Local).AddTicks(2005));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 13, 51, 33, 452, DateTimeKind.Local).AddTicks(2005));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 13, 51, 33, 452, DateTimeKind.Local).AddTicks(2005));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 13, 51, 33, 452, DateTimeKind.Local).AddTicks(2005));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 13, 51, 33, 452, DateTimeKind.Local).AddTicks(2005));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 13, 51, 33, 452, DateTimeKind.Local).AddTicks(2005));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 13, 51, 33, 452, DateTimeKind.Local).AddTicks(2005));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 13, 51, 33, 452, DateTimeKind.Local).AddTicks(2005));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 13, 51, 33, 452, DateTimeKind.Local).AddTicks(2005));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 13, 51, 33, 452, DateTimeKind.Local).AddTicks(2005));
        }
    }
}
