-- Table: public.news

-- DROP TABLE public.news;

CREATE TABLE public.news
(
    source_id character varying(255) COLLATE pg_catalog."default",
    author character varying(255) COLLATE pg_catalog."default",
    date date,
    link character varying COLLATE pg_catalog."default",
    text character varying COLLATE pg_catalog."default"
)
WITH (
    OIDS = TRUE
)
TABLESPACE pg_default;

ALTER TABLE public.news
    OWNER to postgres;