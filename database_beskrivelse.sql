------------------------------------------------- Tabelloppsett -------------------------------------------------
CREATE TABLE Alarm
(
  AlarmID   INTEGER     NOT NULL GENERATED ALWAYS AS IDENTITY UNIQUE,
  Alarmtype VARCHAR(25) NOT NULL,
  Dato      TIMESTAMP   NOT NULL,
  KortID    VARCHAR(4)  NOT NULL,
  NummerID  VARCHAR(4)  NOT NULL,
  PRIMARY KEY (AlarmID)
);

CREATE TABLE Bruker
(
  BrukerID      INTEGER       NOT NULL GENERATED ALWAYS AS IDENTITY UNIQUE,
  Etternavn     VARCHAR(255)  NOT NULL,
  Fornavn       VARCHAR(255)  NOT NULL,
  Epost_adresse VARCHAR(255) ,
  KortID        VARCHAR(4)    UNIQUE,
  PIN           VARCHAR(4)   ,
  GyldighetFRA  TIMESTAMP     DEFAULT '-infinity'::timestamp,
  GyldighetTIL  TIMESTAMP     DEFAULT 'infinity'::timestamp,
  PRIMARY KEY (BrukerID)
);

CREATE TABLE Bruker_KortleserMM
(
  TilgangID INTEGER    NOT NULL GENERATED ALWAYS AS IDENTITY UNIQUE,
  BrukerID  INTEGER    NOT NULL,
  NummerID  VARCHAR(4) NOT NULL,
  PRIMARY KEY (TilgangID)
);

CREATE TABLE Forespørsler
(
  ForespørselID INTEGER     NOT NULL GENERATED ALWAYS AS IDENTITY UNIQUE,
  Status        VARCHAR(25) NOT NULL,
  Dato          TIMESTAMP   NOT NULL,
  KortID        VARCHAR(4)  NOT NULL,
  NummerID      VARCHAR(4)  NOT NULL,
  PRIMARY KEY (ForespørselID)
);

CREATE TABLE Kortleser
(
  NummerID  VARCHAR(4) NOT NULL UNIQUE,
  Romnummer VARCHAR(3) NOT NULL,
  PRIMARY KEY (NummerID)
);

ALTER TABLE Alarm
  ADD CONSTRAINT FK_Kortleser_TO_Alarm
    FOREIGN KEY (NummerID)
    REFERENCES Kortleser (NummerID);

ALTER TABLE Forespørsler
  ADD CONSTRAINT FK_Kortleser_TO_Forespørsler
    FOREIGN KEY (NummerID)
    REFERENCES Kortleser (NummerID);

ALTER TABLE Bruker_KortleserMM
  ADD CONSTRAINT FK_Bruker_TO_Bruker_KortleserMM
    FOREIGN KEY (BrukerID)
    REFERENCES Bruker (BrukerID);

ALTER TABLE Bruker_KortleserMM
  ADD CONSTRAINT FK_Kortleser_TO_Bruker_KortleserMM
    FOREIGN KEY (NummerID)
    REFERENCES Kortleser (NummerID);
----------------------------------------------- Tabelloppsett (slutt) --------------------------------------------------------------------------------------------------

----------------------------------------------- Insetting av variablar til tabellane ---------------------------------------------------------------------------------
-- Legge inn standard brukarar
INSERT INTO bruker(etternavn, fornavn, epost_adresse, kortid, pin, gyldighetfra, gyldighettil) 
VALUES
('Gjøstein', 'Trym', 'Trym.gj@hotmail.com', '0000', '0000', '2021-09-07 12:15:00','2023-09-07 12:15:00'),
('Markeset', 'Gaute', 'peter@hotmail.com', '1111', '0001', '2021-09-07 12:15:00','2023-09-07 12:15:00'),
('Strømmen', 'Isak', 'tullemail@hotmail.com', '2222', '0010', '2021-09-07 12:15:00','2023-09-07 12:15:00'),
('Styve', 'Kristian', 'dragongangsta@hotmail.com', '3333', '0011', '2023-08-07 12:15:00','2023-09-07 12:15:00');

-- Legge inn kortlesarar   
INSERT INTO kortleser(nummerid, romnummer) VALUES
('1000','000'),
('1001','001'),
('1002','002'),
('1003','003');

-- Legge inn tilgongar for dei ulike brukarane (Pass på at brukerID er riktig etter at brukarane er lagt lagt inn i databasen) 
INSERT INTO Bruker_KortleserMM(BrukerID,NummerID) VALUES
(1,'1000'), 
(1,'1001'),
(2,'1001'),-- eksempel her har brukar med brukarID 2 tilgang til kortleseren med kortlserid 1001
(3,'1002'),
(4,'1003');
----------------------------------------------- Insetting av variablar til tabellane (slutt) ---------------------------------------------------------------------------------

----------------------------------------------- Oppsett av views ---------------------------------------------------------------------------------
CREATE VIEW AdgangView AS -- Bruker denne for å sjekke om kortskanning er true eller false
    SELECT kortleser.nummerid, kortleser.romnummer, bruker.PIN, bruker.kortid, bruker.GyldighetFRA, bruker.gyldighetTIL 
        FROM kortleser, bruker, Bruker_KortleserMM
			    WHERE bruker.brukerid = Bruker_KortleserMM.brukerid and kortleser.nummerid = Bruker_KortleserMM.nummerid;



CREATE VIEW Brukertilgang AS -- oversikt over kva kortleser og rom brukarar har tilgang til. (formatet på dato eks "2022-08-04 23:44:23" i C#)
    SELECT  bruker.brukerid,bruker.KortID, Bruker_KortleserMM.TilgangID, bruker.fornavn,bruker.etternavn, kortleser.romnummer, kortleser.nummerid
        FROM bruker, Bruker_KortleserMM, kortleser
		      WHERE bruker.brukerid = Bruker_kortleserMM.brukerid and kortleser.nummerid = Bruker_KortleserMM.NummerID
		        ORDER BY bruker.brukerid;

-- lager eit view som vi kan spørre etter adganger med romnummer
CREATE VIEW AdgangRom AS
	SELECT kortleser.romnummer, date(forespørsler.dato), min(CAST(forespørsler.dato AS time)), max(CAST(forespørsler.dato AS time))
	  FROM forespørsler, kortleser
	    WHERE kortleser.nummerid = forespørsler.nummerid
	      Group by kortleser.romnummer, date(forespørsler.dato);

----------------------------------------- Oppsett av views (slutt) ----------------------------------------------------------------------------------






----------------------------------------- Diverse kode brukt i systemet, og til testing (ikkje ein del av databasen) -----------------------------------------------------------
-- Rapport generering
-- Liste av brukerdata på grunnlag av brukernamn (Tolker brukernamn som kortid)
SELECT * FROM bruker WHERE kortid = 'kortid'; -- Antar at kortid er det som er meint med brukernamn 

-- liste adgangslogg basert på brukernamn og datoer fra-til
SELECT forespørsler.ForespørselID, forespørsler.Status, forespørsler.Dato, forespørsler.kortid, forespørsler.NummerID
  FROM forespørsler, bruker
  	WHERE bruker.kortid = forespørsler.kortid
    	ORDER BY forespørsler.kortid, bruker.gyldighetfra, bruker.GyldighetTIL; 

-- Liste alle avviste innpasseringsforsøk basert på kva dør som er oppgitt
SELECT forespørsler.ForespørselID, forespørsler.Status, forespørsler.Dato, forespørsler.kortid, forespørsler.NummerID, kortleser.romnummer
  FROM forespørsler, bruker, kortleser
  	WHERE bruker.kortid = forespørsler.kortid AND kortleser.nummerid = forespørsler.nummerid AND Status = 'Avvist' AND kortleser.romnummer = 'sett inn dørnummer'
    


-- Liste alarmer mellom to datoer
SELECT * FROM alarmer
  WHERE dato >= 'datofra' AND dato <= 'datotil';

-- Får første og siste adgang per dag basert på romnummer
	--SELECT date(forespørsler.dato), min(forespørsler.dato), max(forespørsler.dato)
	--FROM forespørsler, kortleser
	--WHERE kortleser.romnummer = '000' AND kortleser.nummerid = forespørsler.nummerid
	--Group by date(forespørsler.dato)

              -- Kan velge romnummer med
              select * from adgangrom where romnummer = '';
              
-- Forskjellige select
SELECT * from brukertilgang;
SELECT * from adgangview;
select * from kortleser;
select * from bruker_kortlesermm;


-- Diverse bra og kunne kodar
TRUNCATE TABLE tablenamn; -- Slettar alt innholdet, og resettar identiteten


INSERT INTO forespørsler(status, dato, KortID, NummerID) VALUES
('Godkjent', '2021-10-07 10:15:00', '0000', '1000'),
('Avvist', '2022-11-09 11:15:00', '0000', '1000'),
('Avvist', '2022-13-09 12:20:00', '0000', '1000'),
('Avvist', '2022-13-10 13:15:00', '0000', '1000');
----------------------------------------- Diverse kode brukt i systemet, og til testing (slutt) -----------------------------------------------------------