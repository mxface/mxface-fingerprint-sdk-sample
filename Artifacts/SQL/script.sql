CREATE TABLE IF NOT EXISTS public."__FingerprintSubjects"
(
    "Id" integer NOT NULL DEFAULT nextval('"__FingerprintSubjects_Id_seq"'::regclass),
    "SubjectId" character varying(45) COLLATE pg_catalog."default",
    "Template" bytea,
    "Group" character varying(45) COLLATE pg_catalog."default",
    "ClientId" integer,
    CONSTRAINT "__FingerprintSubjects_pkey" PRIMARY KEY ("Id")
)

CREATE TABLE IF NOT EXISTS "__TemplateTypes"
(
    "Id" TEXT NOT NULL,
    "TemplateName" TEXT,
    "CreatedDate" TEXT NOT NULL,
    "ModifiedDate" TEXT,
    "IsActive" INTEGER NOT NULL,
    PRIMARY KEY ("Id")
);