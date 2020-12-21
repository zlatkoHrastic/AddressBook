CREATE TABLE contact (
	id 			serial,
	name        text NOT NULL,
	date_of_birth	date NOT NULL,
	address		text NOT NULL,
	telephone_numbers	text[],
	CONSTRAINT contact_pkey PRIMARY KEY (id),
	CONSTRAINT uq_name_address UNIQUE (name, address)
);
	
CREATE UNIQUE INDEX ix_contact_name_address ON public.contact USING btree
(name COLLATE pg_catalog."default" ASC NULLS LAST, address COLLATE pg_catalog."default" ASC NULLS LAST)
TABLESPACE pg_default;
	
ALTER TABLE public.contact CLUSTER ON ix_contact_name_address;
	
CREATE UNIQUE INDEX pk_contact ON public.contact USING btree (id ASC NULLS LAST) TABLESPACE pg_default;