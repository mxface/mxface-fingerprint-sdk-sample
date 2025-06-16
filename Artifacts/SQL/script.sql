CREATE TABLE IF NOT EXISTS "__FingerprintSubjects"
(
    "Id" TEXT NOT NULL,
    "SubjectId" TEXT,
    "TemplateTypeId" TEXT,
    "Template" BLOB,
    "CreatedDate" TEXT NOT NULL,
    "ModifiedDate" TEXT,
    "IsActive" INTEGER NOT NULL,
    PRIMARY KEY ("Id"),
    FOREIGN KEY ("TemplateTypeId") REFERENCES "__TemplateTypes" ("Id")
);

CREATE TABLE IF NOT EXISTS "__TemplateTypes"
(
    "Id" TEXT NOT NULL,
    "TemplateName" TEXT,
    "CreatedDate" TEXT NOT NULL,
    "ModifiedDate" TEXT,
    "IsActive" INTEGER NOT NULL,
    PRIMARY KEY ("Id")
);