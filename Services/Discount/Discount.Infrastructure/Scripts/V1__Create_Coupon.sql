CREATE TABLE IF NOT EXISTS coupons (
    id SERIAL PRIMARY KEY,
    productid TEXT NOT NULL,
    description VARCHAR(200) NOT NULL,
    amount INT NOT NULL CHECK (amount > 0),
    version INT NOT NULL DEFAULT 1,
    CONSTRAINT uq_coupons_productid UNIQUE (productid)
);