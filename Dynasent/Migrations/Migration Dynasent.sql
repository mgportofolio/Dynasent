--Table Drivers
CREATE TABLE "Drivers"
(
	"DriversId" INT NOT NULL GENERATED ALWAYS AS IDENTITY,
	"DriversName" VARCHAR NOT NULL,
	"DriversPhoneNumber" VARCHAR NOT NULL,
	CONSTRAINT "PK_Drivers" PRIMARY KEY ("DriversId")
);

--Table Bus
CREATE TABLE "Bus"
(
	"BusId" INT NOT NULL GENERATED ALWAYS AS IDENTITY,
	"BusName" VARCHAR NOT NULL,
	"BusType" VARCHAR NOT NULL,
	"BusNumber" VARCHAR NOT NULL,
	"DriversId" INT NOT NULL,
	"StartTimeStamp" TIMESTAMPTZ DEFAULT NOW() NOT NULL,
	"IsEnd" BOOL DEFAULT 'f' NOT NULL,
	"EndTimeStamp" TIMESTAMPTZ,
			
	CONSTRAINT "PK_Bus" PRIMARY KEY ("BusId"),
	CONSTRAINT "FK_Drivers_Bus" FOREIGN KEY ("DriversId") REFERENCES "Drivers" ("DriversId")
);
	
--Table Passanger	
cREATE TABLE "Passanger"
(
	"PassangerId" INT NOT NULL GENERATED ALWAYS AS IDENTITY,
	"BusId" INT NOT NULL,
	"PassangerName" VARCHAR NOT NULL,
	"PassangerContact" VARCHAR NOT NULL,
	"InOrOut" BOOL DEFAULT 'f' NOT NULL,
	"TimeStamp" TIMESTAMPTZ DEFAULT NOW() NOT NULL,
	"QrCodeSrc" VARCHAR NOT NULL,
	CONSTRAINT "PK_Passanger" PRIMARY KEY ("PassangerId"),
	CONSTRAINT "FK_Bus_Passanger" FOREIGN KEY ("BusId") REFERENCES "Bus" ("BusId")
);

CREATE TABLE "Tokens"
(
	"TokenId" INT NOT NULL GENERATED ALWAYS AS IDENTITY,
	"TokenType" VARCHAR NOT NULL,
	"UniqueCode" VARCHAR NOT NULL,
	"IsExpired" BOOL DEFAULT 'f' NOT NULL,
	"TimeStamp" TIMESTAMPTZ DEFAULT NOW() NOT NULL,
	CONSTRAINT "PK_Token" PRIMARY KEY ("TokenId")
);		

--Sanity Test		
SELECT * FROM "Bus"
SELECT * FROM "Passanger"
SELECT * FROM "Drivers"
SELECT * FROM "Tokens"