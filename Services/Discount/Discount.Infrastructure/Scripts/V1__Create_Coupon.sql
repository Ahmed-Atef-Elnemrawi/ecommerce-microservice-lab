CREATE TABLE IF NOT EXISTS "Coupons"(
    "Id" SERIAL PRIMARY KEY,
    "ProductId" TEXT NOT NULL,
    "Description" VARCHAR(200) NOT NULL,
    "Amount" INT NOT NULL CHECK ("Amount" > 0),
    "Version" INT NOT NULL DEFAULT 1,

    CONSTRAINT "UQ_Coupons_ProductId" UNIQUE ("ProductId")
);