-- Create Category Table
CREATE TABLE Categories (
    CategoryID INTEGER PRIMARY KEY AUTOINCREMENT,
    CategoryName TEXT NOT NULL
);

-- Create SubCategory Table
CREATE TABLE SubCategories (
    SubItemID INTEGER PRIMARY KEY AUTOINCREMENT,
    SubItemName TEXT NOT NULL,
    CategoryID INTEGER,
    ParentSubItemID INTEGER NULL, -- Allows hierarchical subcategories
    Price DECIMAL(10,2) NULL, -- Only relevant for nested subcategories
    FOREIGN KEY (CategoryID) REFERENCES Categories(CategoryID),
    FOREIGN KEY (ParentSubItemID) REFERENCES SubCategories(SubItemID)
);
