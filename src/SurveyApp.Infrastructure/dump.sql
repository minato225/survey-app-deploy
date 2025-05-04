--
-- PostgreSQL database dump
--

-- Dumped from database version 17.3 (Debian 17.3-1.pgdg120+1)
-- Dumped by pg_dump version 17.3 (Debian 17.3-1.pgdg120+1)

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
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
-- Name: AdminUsers; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."AdminUsers" (
                                     "Id" uuid NOT NULL,
                                     "Login" text NOT NULL,
                                     "PasswordHash" text NOT NULL,
                                     "Email" text NOT NULL,
                                     "RefreshToken" text,
                                     "RefreshTokenExpiryTime" timestamp with time zone
);


ALTER TABLE public."AdminUsers" OWNER TO postgres;

--
-- Name: Forms; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Forms" (
                                "Id" uuid NOT NULL,
                                "Name" text NOT NULL,
                                "Description" text,
                                "Link" text,
                                "AdminUserId" uuid NOT NULL
);


ALTER TABLE public."Forms" OWNER TO postgres;

--
-- Name: Questions; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Questions" (
                                    "Id" uuid NOT NULL,
                                    "Title" text NOT NULL,
                                    "Type" integer NOT NULL,
                                    "IsRequired" boolean NOT NULL,
                                    "FormId" uuid NOT NULL
);


ALTER TABLE public."Questions" OWNER TO postgres;

--
-- Name: UserAnswers; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."UserAnswers" (
                                      "Id" uuid NOT NULL,
                                      "QuestionId" uuid NOT NULL,
                                      "VariationSetId" uuid,
                                      "Text" text NOT NULL,
                                      "Date" timestamp with time zone
);


ALTER TABLE public."UserAnswers" OWNER TO postgres;

--
-- Name: VariationSets; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."VariationSets" (
                                        "Id" uuid NOT NULL,
                                        "Text" text NOT NULL,
                                        "QuestionId" uuid NOT NULL
);


ALTER TABLE public."VariationSets" OWNER TO postgres;

--
-- Name: __EFMigrationsHistory; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."__EFMigrationsHistory" (
                                                "MigrationId" character varying(150) NOT NULL,
                                                "ProductVersion" character varying(32) NOT NULL
);


ALTER TABLE public."__EFMigrationsHistory" OWNER TO postgres;


ALTER TABLE ONLY public."AdminUsers"
    ADD CONSTRAINT "PK_AdminUsers" PRIMARY KEY ("Id");


--
-- Name: Forms PK_Forms; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Forms"
    ADD CONSTRAINT "PK_Forms" PRIMARY KEY ("Id");


--
-- Name: Questions PK_Questions; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Questions"
    ADD CONSTRAINT "PK_Questions" PRIMARY KEY ("Id");


--
-- Name: UserAnswers PK_UserAnswers; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."UserAnswers"
    ADD CONSTRAINT "PK_UserAnswers" PRIMARY KEY ("Id");


--
-- Name: VariationSets PK_VariationSets; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."VariationSets"
    ADD CONSTRAINT "PK_VariationSets" PRIMARY KEY ("Id");


--
-- Name: __EFMigrationsHistory PK___EFMigrationsHistory; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."__EFMigrationsHistory"
    ADD CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId");


--
-- Name: IX_Forms_AdminUserId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_Forms_AdminUserId" ON public."Forms" USING btree ("AdminUserId");


--
-- Name: IX_Questions_FormId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_Questions_FormId" ON public."Questions" USING btree ("FormId");


--
-- Name: IX_UserAnswers_QuestionId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_UserAnswers_QuestionId" ON public."UserAnswers" USING btree ("QuestionId");


--
-- Name: IX_UserAnswers_VariationSetId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_UserAnswers_VariationSetId" ON public."UserAnswers" USING btree ("VariationSetId");


--
-- Name: IX_VariationSets_QuestionId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_VariationSets_QuestionId" ON public."VariationSets" USING btree ("QuestionId");


--
-- Name: Forms FK_Forms_AdminUsers_AdminUserId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Forms"
    ADD CONSTRAINT "FK_Forms_AdminUsers_AdminUserId" FOREIGN KEY ("AdminUserId") REFERENCES public."AdminUsers"("Id") ON DELETE CASCADE;


--
-- Name: Questions FK_Questions_Forms_FormId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Questions"
    ADD CONSTRAINT "FK_Questions_Forms_FormId" FOREIGN KEY ("FormId") REFERENCES public."Forms"("Id") ON DELETE CASCADE;


--
-- Name: UserAnswers FK_UserAnswers_Questions_QuestionId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."UserAnswers"
    ADD CONSTRAINT "FK_UserAnswers_Questions_QuestionId" FOREIGN KEY ("QuestionId") REFERENCES public."Questions"("Id") ON DELETE CASCADE;


--
-- Name: UserAnswers FK_UserAnswers_VariationSets_VariationSetId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."UserAnswers"
    ADD CONSTRAINT "FK_UserAnswers_VariationSets_VariationSetId" FOREIGN KEY ("VariationSetId") REFERENCES public."VariationSets"("Id") ON DELETE SET NULL;


--
-- Name: VariationSets FK_VariationSets_Questions_QuestionId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."VariationSets"
    ADD CONSTRAINT "FK_VariationSets_Questions_QuestionId" FOREIGN KEY ("QuestionId") REFERENCES public."Questions"("Id") ON DELETE CASCADE;


--
-- PostgreSQL database dump complete
--

