--
-- PostgreSQL database dump
--

\restrict 5Te5wRcWXKhI1WRABQyVTUjl5ttaZNufhSS5tWtifQUjvFDNKp4K8WTiLmidcmy

-- Dumped from database version 18.4 (Debian 18.4-1.pgdg13+1)
-- Dumped by pg_dump version 18.4 (Debian 18.4-1.pgdg13+1)

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET transaction_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- Name: Booking; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Booking" (
    "BookingId" uuid NOT NULL,
    "RoomId" uuid NOT NULL,
    "UserId" uuid NOT NULL,
    "StartTime" timestamp with time zone NOT NULL,
    "EndTime" timestamp with time zone NOT NULL,
    "Status" character varying(20) NOT NULL,
    "CreatedAt" timestamp with time zone NOT NULL,
    CONSTRAINT "CK_Booking_EndAfterStart" CHECK (("EndTime" > "StartTime"))
);


ALTER TABLE public."Booking" OWNER TO postgres;

--
-- Name: Buildings; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Buildings" (
    "BuildingId" uuid NOT NULL,
    "Name" text,
    "Address_Street" character varying(200),
    "Address_City" character varying(100),
    "Address_PostalCode" character varying(20),
    "CreatedAt" timestamp with time zone NOT NULL
);


ALTER TABLE public."Buildings" OWNER TO postgres;

--
-- Name: Company; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Company" (
    "CompanyId" uuid NOT NULL,
    "Name" text NOT NULL,
    "RegisterNum" text,
    "CreatedAt" timestamp with time zone NOT NULL
);


ALTER TABLE public."Company" OWNER TO postgres;

--
-- Name: Employees; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Employees" (
    "Id" uuid NOT NULL,
    "FullName" text NOT NULL,
    "HireDate" timestamp with time zone NOT NULL
);


ALTER TABLE public."Employees" OWNER TO postgres;

--
-- Name: Managers; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Managers" (
    "Id" uuid NOT NULL,
    "TeamSize" integer NOT NULL
);


ALTER TABLE public."Managers" OWNER TO postgres;

--
-- Name: Regulars; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Regulars" (
    "Id" uuid NOT NULL,
    "Grade" text
);


ALTER TABLE public."Regulars" OWNER TO postgres;

--
-- Name: Room; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Room" (
    "RoomId" uuid NOT NULL,
    "BuildingId" uuid NOT NULL,
    "Number" text,
    "Capacity" integer NOT NULL,
    "CreatedAt" timestamp with time zone NOT NULL
);


ALTER TABLE public."Room" OWNER TO postgres;

--
-- Name: RoomTags; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."RoomTags" (
    "RoomId" uuid NOT NULL,
    "TagId" uuid NOT NULL,
    "AssignedAt" timestamp with time zone NOT NULL
);


ALTER TABLE public."RoomTags" OWNER TO postgres;

--
-- Name: Tag; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Tag" (
    "TagId" uuid NOT NULL,
    "Name" character varying(50) NOT NULL
);


ALTER TABLE public."Tag" OWNER TO postgres;

--
-- Name: UserProfiles; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."UserProfiles" (
    "Id" uuid CONSTRAINT "UserProfile_Id_not_null" NOT NULL,
    "UserId" uuid CONSTRAINT "UserProfile_UserId_not_null" NOT NULL,
    "AvatarUrl" text,
    "Bio" text,
    "PhoneNumber" text
);


ALTER TABLE public."UserProfiles" OWNER TO postgres;

--
-- Name: Users; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Users" (
    "UserId" uuid NOT NULL,
    "CompanyId" uuid NOT NULL,
    "Email" text,
    "FirstName" text,
    "LastName" text,
    "ManagerId" uuid,
    "IsDeleted" boolean NOT NULL,
    "CreatedAt" timestamp with time zone NOT NULL
);


ALTER TABLE public."Users" OWNER TO postgres;

--
-- Name: __EFMigrationsHistory; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL
);


ALTER TABLE public."__EFMigrationsHistory" OWNER TO postgres;

--
-- Data for Name: Booking; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Booking" ("BookingId", "RoomId", "UserId", "StartTime", "EndTime", "Status", "CreatedAt") FROM stdin;
019feb52-51d1-76c6-9cd0-da0dd8f9291c	f1fec78e-124b-4480-9de7-dd921cef5484	019feb44-b70f-7829-b14d-3ffca54d751f	2026-08-11 10:00:00+00	2026-08-11 11:00:00+00	Pending	2026-08-10 10:57:48.040211+00
019feb57-ed5b-78cf-b193-f423b9ab7280	f1fec78e-124b-4480-9de7-dd921cef5484	019feb44-b70f-7829-b14d-3ffca54d751f	2026-08-12 10:00:00+00	2026-08-12 11:00:00+00	Pending	2026-08-10 11:03:55.991111+00
\.


--
-- Data for Name: Buildings; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Buildings" ("BuildingId", "Name", "Address_Street", "Address_City", "Address_PostalCode", "CreatedAt") FROM stdin;
019feb47-29bd-74ec-b9b6-3447a3edf7a7	Main Office	Test Street 1	Warsaw	00-001	2026-08-10 10:45:37.337508+00
\.


--
-- Data for Name: Company; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Company" ("CompanyId", "Name", "RegisterNum", "CreatedAt") FROM stdin;
63034758-f5b0-4e91-8fd8-825f4ba908c2	Test Company	REG-001	2026-08-10 10:40:18.14932+00
\.


--
-- Data for Name: Employees; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Employees" ("Id", "FullName", "HireDate") FROM stdin;
\.


--
-- Data for Name: Managers; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Managers" ("Id", "TeamSize") FROM stdin;
\.


--
-- Data for Name: Regulars; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Regulars" ("Id", "Grade") FROM stdin;
\.


--
-- Data for Name: Room; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Room" ("RoomId", "BuildingId", "Number", "Capacity", "CreatedAt") FROM stdin;
f1fec78e-124b-4480-9de7-dd921cef5484	019feb47-29bd-74ec-b9b6-3447a3edf7a7	101	4	2026-08-10 10:47:10.485785+00
\.


--
-- Data for Name: RoomTags; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."RoomTags" ("RoomId", "TagId", "AssignedAt") FROM stdin;
\.


--
-- Data for Name: Tag; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Tag" ("TagId", "Name") FROM stdin;
\.


--
-- Data for Name: UserProfiles; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."UserProfiles" ("Id", "UserId", "AvatarUrl", "Bio", "PhoneNumber") FROM stdin;
\.


--
-- Data for Name: Users; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Users" ("UserId", "CompanyId", "Email", "FirstName", "LastName", "ManagerId", "IsDeleted", "CreatedAt") FROM stdin;
019feb44-b70f-7829-b14d-3ffca54d751f	63034758-f5b0-4e91-8fd8-825f4ba908c2	test@example.com	Ivan	Petrov	\N	f	2026-08-10 10:42:56.876627+00
\.


--
-- Data for Name: __EFMigrationsHistory; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."__EFMigrationsHistory" ("MigrationId", "ProductVersion") FROM stdin;
20260808170505_Initil	10.0.10
20260810105645_RemoveBookingRowVersion	10.0.10
\.


--
-- Name: Booking PK_Booking; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Booking"
    ADD CONSTRAINT "PK_Booking" PRIMARY KEY ("BookingId");


--
-- Name: Buildings PK_Buildings; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Buildings"
    ADD CONSTRAINT "PK_Buildings" PRIMARY KEY ("BuildingId");


--
-- Name: Company PK_Company; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Company"
    ADD CONSTRAINT "PK_Company" PRIMARY KEY ("CompanyId");


--
-- Name: Employees PK_Employees; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Employees"
    ADD CONSTRAINT "PK_Employees" PRIMARY KEY ("Id");


--
-- Name: Managers PK_Managers; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Managers"
    ADD CONSTRAINT "PK_Managers" PRIMARY KEY ("Id");


--
-- Name: Regulars PK_Regulars; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Regulars"
    ADD CONSTRAINT "PK_Regulars" PRIMARY KEY ("Id");


--
-- Name: Room PK_Room; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Room"
    ADD CONSTRAINT "PK_Room" PRIMARY KEY ("RoomId");


--
-- Name: RoomTags PK_RoomTags; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."RoomTags"
    ADD CONSTRAINT "PK_RoomTags" PRIMARY KEY ("RoomId", "TagId");


--
-- Name: Tag PK_Tag; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Tag"
    ADD CONSTRAINT "PK_Tag" PRIMARY KEY ("TagId");


--
-- Name: UserProfiles PK_UserProfiles; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."UserProfiles"
    ADD CONSTRAINT "PK_UserProfiles" PRIMARY KEY ("Id");


--
-- Name: Users PK_Users; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Users"
    ADD CONSTRAINT "PK_Users" PRIMARY KEY ("UserId");


--
-- Name: __EFMigrationsHistory PK___EFMigrationsHistory; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."__EFMigrationsHistory"
    ADD CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId");


--
-- Name: IX_Booking_RoomId_StartTime; Type: INDEX; Schema: public; Owner: postgres
--

CREATE UNIQUE INDEX "IX_Booking_RoomId_StartTime" ON public."Booking" USING btree ("RoomId", "StartTime");


--
-- Name: IX_Booking_UserId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_Booking_UserId" ON public."Booking" USING btree ("UserId");


--
-- Name: IX_RoomTags_TagId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_RoomTags_TagId" ON public."RoomTags" USING btree ("TagId");


--
-- Name: IX_Room_BuildingId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_Room_BuildingId" ON public."Room" USING btree ("BuildingId");


--
-- Name: IX_Tag_Name; Type: INDEX; Schema: public; Owner: postgres
--

CREATE UNIQUE INDEX "IX_Tag_Name" ON public."Tag" USING btree ("Name");


--
-- Name: IX_UserProfiles_UserId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE UNIQUE INDEX "IX_UserProfiles_UserId" ON public."UserProfiles" USING btree ("UserId");


--
-- Name: IX_Users_CompanyId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_Users_CompanyId" ON public."Users" USING btree ("CompanyId");


--
-- Name: IX_Users_ManagerId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_Users_ManagerId" ON public."Users" USING btree ("ManagerId");


--
-- Name: Booking FK_Booking_Room_RoomId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Booking"
    ADD CONSTRAINT "FK_Booking_Room_RoomId" FOREIGN KEY ("RoomId") REFERENCES public."Room"("RoomId") ON DELETE RESTRICT;


--
-- Name: Booking FK_Booking_Users_UserId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Booking"
    ADD CONSTRAINT "FK_Booking_Users_UserId" FOREIGN KEY ("UserId") REFERENCES public."Users"("UserId") ON DELETE RESTRICT;


--
-- Name: Managers FK_Managers_Employees_Id; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Managers"
    ADD CONSTRAINT "FK_Managers_Employees_Id" FOREIGN KEY ("Id") REFERENCES public."Employees"("Id") ON DELETE CASCADE;


--
-- Name: Regulars FK_Regulars_Employees_Id; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Regulars"
    ADD CONSTRAINT "FK_Regulars_Employees_Id" FOREIGN KEY ("Id") REFERENCES public."Employees"("Id") ON DELETE CASCADE;


--
-- Name: RoomTags FK_RoomTags_Room_RoomId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."RoomTags"
    ADD CONSTRAINT "FK_RoomTags_Room_RoomId" FOREIGN KEY ("RoomId") REFERENCES public."Room"("RoomId") ON DELETE RESTRICT;


--
-- Name: RoomTags FK_RoomTags_Tag_TagId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."RoomTags"
    ADD CONSTRAINT "FK_RoomTags_Tag_TagId" FOREIGN KEY ("TagId") REFERENCES public."Tag"("TagId") ON DELETE CASCADE;


--
-- Name: Room FK_Room_Buildings_BuildingId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Room"
    ADD CONSTRAINT "FK_Room_Buildings_BuildingId" FOREIGN KEY ("BuildingId") REFERENCES public."Buildings"("BuildingId") ON DELETE CASCADE;


--
-- Name: UserProfiles FK_UserProfiles_Users_UserId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."UserProfiles"
    ADD CONSTRAINT "FK_UserProfiles_Users_UserId" FOREIGN KEY ("UserId") REFERENCES public."Users"("UserId") ON DELETE CASCADE;


--
-- Name: Users FK_Users_Company_CompanyId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Users"
    ADD CONSTRAINT "FK_Users_Company_CompanyId" FOREIGN KEY ("CompanyId") REFERENCES public."Company"("CompanyId") ON DELETE RESTRICT;


--
-- Name: Users FK_Users_Users_ManagerId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Users"
    ADD CONSTRAINT "FK_Users_Users_ManagerId" FOREIGN KEY ("ManagerId") REFERENCES public."Users"("UserId") ON DELETE SET NULL;


--
-- PostgreSQL database dump complete
--

\unrestrict 5Te5wRcWXKhI1WRABQyVTUjl5ttaZNufhSS5tWtifQUjvFDNKp4K8WTiLmidcmy

