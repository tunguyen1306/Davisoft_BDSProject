
--
-- Create schema subaru
--

CREATE DATABASE IF NOT EXISTS subaru;
USE subaru;

--
-- Definition of table `abbpvdeds`
-- CPF Deduction, Loan Deduction and Other Deduction for bank payment voucher
CREATE TABLE `abbpvdeds` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `No` longtext,
  `CAmount` decimal(18,2) NOT NULL,
  `LAmount` decimal(18,2) NOT NULL,
  `OAmount` decimal(18,2) NOT NULL,
  `BPVID` int(11) NOT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID` (`ID`),
  KEY `BPVID` (`BPVID`),
  CONSTRAINT `AbBPVDed_BPV` FOREIGN KEY (`BPVID`) REFERENCES `bpvs` (`ID`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;


--
-- Definition of table `accessorydiscounts`
-- To save accessory discount
-- Used for booking
--

CREATE TABLE `accessorydiscounts` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `ModelIDs` longtext,
  `ModelCodes` longtext,
  `ModelColors` longtext,
  `ModelYears` longtext,
  `SerialNumber` longtext,
  `AccessoryType` longtext,
  `Description` longtext,
  `DiscountValue` decimal(18,2) NOT NULL,
  `Remarks` longtext,
  `StartTime` datetime NOT NULL,
  `EndTime` datetime NOT NULL,
  `CreatedBy` longtext,
  `_Created` datetime NOT NULL,
  `_Updated` datetime DEFAULT NULL,
  `_Deleted` datetime DEFAULT NULL,
  `EntityStatus_Value` longtext,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;


--
-- Definition of table `adjustments`
-- To adjust amount for Invoice and FC Invoice
--
CREATE TABLE `adjustments` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `AdjustmentNo` longtext,
  `AdjustmentDate` datetime NOT NULL,
  `AdjustmentType_Value` longtext,
  `AdjustmentBy_Value` longtext,
  `AdjustmentFor_Value` longtext,
  `Amount` decimal(18,2) NOT NULL,
  `Remark` longtext,
  `InvoiceID` int(11) DEFAULT NULL,
  `FcInvoiceID` int(11) DEFAULT NULL,
  `CustomerID` int(11) DEFAULT NULL,
  `FinanceID` int(11) DEFAULT NULL,
  `PaymentID` int(11) NOT NULL,
  `_Created` datetime NOT NULL,
  `_Updated` datetime DEFAULT NULL,
  `_Deleted` datetime DEFAULT NULL,
  `EntityStatus_Value` longtext,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID` (`ID`),
  KEY `InvoiceID` (`InvoiceID`),
  KEY `FcInvoiceID` (`FcInvoiceID`),
  KEY `CustomerID` (`CustomerID`),
  KEY `FinanceID` (`FinanceID`),
  KEY `PaymentID` (`PaymentID`),
  CONSTRAINT `Adjustment_Customer` FOREIGN KEY (`CustomerID`) REFERENCES `customers` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `Adjustment_FcInvoice` FOREIGN KEY (`FcInvoiceID`) REFERENCES `fcinvoices` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `Adjustment_Finance` FOREIGN KEY (`FinanceID`) REFERENCES `financecompanies` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `Adjustment_Invoice` FOREIGN KEY (`InvoiceID`) REFERENCES `invoices` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `Adjustment_Payment` FOREIGN KEY (`PaymentID`) REFERENCES `payments` (`ID`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;


--
-- Definition of table `allocates`
-- To save date of booking allocation
--
CREATE TABLE `allocates` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Date` datetime NOT NULL,
  `DDate` datetime NOT NULL,
  `Remark` longtext,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID` (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8;

--
-- Definition of table `bookingcleanings`
-- To book clearning vehicle
--
CREATE TABLE `bookingcleanings` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Date` datetime NOT NULL,
  `SlotTime_Value` longtext,
  `Remarks` longtext,
  `CleaningID` int(11) NOT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID` (`ID`),
  KEY `CleaningID` (`CleaningID`),
  CONSTRAINT `BookingCleaning_Cleaning` FOREIGN KEY (`CleaningID`) REFERENCES `cleanings` (`ID`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;


--
-- Definition of table `bookingcollections`
-- Records of collection slot of booking.
CREATE TABLE `bookingcollections` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Date` datetime NOT NULL,
  `SlotTime_Value` longtext,
  `Remarks` longtext,
  `CollectionID` int(11) NOT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID` (`ID`),
  KEY `CollectionID` (`CollectionID`),
  CONSTRAINT `BookingCollection_Collection` FOREIGN KEY (`CollectionID`) REFERENCES `collections` (`ID`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;


--
-- Definition of table `bookingdeliveries`
-- To book delivery vehicle
--
CREATE TABLE `bookingdeliveries` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Date` datetime NOT NULL,
  `SlotTime_Value` longtext,
  `Remarks` longtext,
  `CollectionID` int(11) NOT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID` (`ID`),
  KEY `CollectionID` (`CollectionID`),
  CONSTRAINT `BookingDelivery_Collection` FOREIGN KEY (`CollectionID`) REFERENCES `collections` (`ID`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;


--
-- Definition of table `bookingdiscountitems`
-- To save discount items related to booking.
--
CREATE TABLE `bookingdiscountitems` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `DiscountValue` decimal(18,2) NOT NULL,
  `Description` longtext,
  `ModelDiscountID` int(11) NOT NULL,
  `Booking_ID` int(11) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID` (`ID`),
  KEY `ModelDiscountID` (`ModelDiscountID`),
  KEY `Booking_ID` (`Booking_ID`),
  CONSTRAINT `BookingDiscountItem_ModelDiscount` FOREIGN KEY (`ModelDiscountID`) REFERENCES `modeldiscounts` (`ID`) ON DELETE CASCADE ON UPDATE NO ACTION,
  CONSTRAINT `Booking_ModelDiscounts` FOREIGN KEY (`Booking_ID`) REFERENCES `bookings` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;


--
-- Definition of table `bookingoptionalitems`
-- To save Optional Accessories related to booking
--
CREATE TABLE `bookingoptionalitems` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Price` decimal(18,2) NOT NULL,
  `OptionalItemID` int(11) NOT NULL,
  `Booking_ID` int(11) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID` (`ID`),
  KEY `OptionalItemID` (`OptionalItemID`),
  KEY `Booking_ID` (`Booking_ID`),
  CONSTRAINT `BookingOptionalItem_OptionalItem` FOREIGN KEY (`OptionalItemID`) REFERENCES `optionalitems` (`ID`) ON DELETE CASCADE ON UPDATE NO ACTION,
  CONSTRAINT `Booking_BookingOptionalItems` FOREIGN KEY (`Booking_ID`) REFERENCES `bookings` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=27 DEFAULT CHARSET=utf8;


--
-- Definition of table `bookingpaymentdetails`
-- payment details items for each booking
--
CREATE TABLE `bookingpaymentdetails` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `BookingTempPaymentID` int(11) NOT NULL,
  `Amount` decimal(18,2) NOT NULL,
  `BookingRealPaymentID` int(11) NOT NULL,
  `_Created` datetime NOT NULL,
  `_Updated` datetime DEFAULT NULL,
  `_Deleted` datetime DEFAULT NULL,
  `EntityStatus_Value` longtext,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID` (`ID`),
  KEY `BookingTempPaymentID` (`BookingTempPaymentID`),
  KEY `BookingRealPaymentID` (`BookingRealPaymentID`),
  CONSTRAINT `BookingPaymentDetail_RealPayment` FOREIGN KEY (`BookingRealPaymentID`) REFERENCES `bookingpayments` (`ID`) ON DELETE CASCADE ON UPDATE NO ACTION,
  CONSTRAINT `BookingPaymentDetail_TempPayment` FOREIGN KEY (`BookingTempPaymentID`) REFERENCES `bookingpayments` (`ID`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8;


--
-- Definition of table `bookingpayments`
-- deposit payment related to booking.
--
CREATE TABLE `bookingpayments` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Amount` decimal(18,2) NOT NULL,
  `UsedAmount` decimal(18,2) NOT NULL,
  `DepositNo` longtext,
  `Remark` longtext,
  `BookingPaymentType_Value` longtext,
  `BookingPaymentStatus_Value` longtext,
  `PaymentDate` datetime NOT NULL,
  `CustomerID` int(11) DEFAULT NULL,
  `TransferNumber` longtext,
  `ChequeNumber` longtext,
  `BankTransferNumber` longtext,
  `Voucher` longtext,
  `Discriminator` varchar(128) NOT NULL,
  `Booking_ID` int(11) DEFAULT NULL,
  `PaymentPayee_Value` longtext,
  `DisbursementDate` datetime NOT NULL,
  `CreditCardNo` longtext,
  `ParfcoeNumber` longtext,
  `IsRegistrationPayment` tinyint(1) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID` (`ID`),
  KEY `CustomerID` (`CustomerID`),
  KEY `Booking_ID` (`Booking_ID`),
  CONSTRAINT `BookingPayment_Customer` FOREIGN KEY (`CustomerID`) REFERENCES `customers` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `Booking_Payments` FOREIGN KEY (`Booking_ID`) REFERENCES `bookings` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=44 DEFAULT CHARSET=utf8;


--
-- Definition of table `bookingpromotionitems`
-- promotion items for each booking (if any)
--
CREATE TABLE `bookingpromotionitems` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Price` decimal(18,2) NOT NULL,
  `PromotionItemID` int(11) NOT NULL,
  `Booking_ID` int(11) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID` (`ID`),
  KEY `PromotionItemID` (`PromotionItemID`),
  KEY `Booking_ID` (`Booking_ID`),
  CONSTRAINT `BookingPromotionItem_PromotionItem` FOREIGN KEY (`PromotionItemID`) REFERENCES `promotionitems` (`ID`) ON DELETE CASCADE ON UPDATE NO ACTION,
  CONSTRAINT `Booking_BookingPromotionItems` FOREIGN KEY (`Booking_ID`) REFERENCES `bookings` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8;


--
-- Definition of table `bookings`
-- data info for booking
--
CREATE TABLE `bookings` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `ContractNumber` longtext,
  `BookingDate` datetime NOT NULL,
  `ExpectedDate` datetime NOT NULL,
  `SSSC` tinyint(1) NOT NULL,
  `RegisteredAddress` longtext,
  `CarModelID` int(11) NOT NULL,
  `CarColorID` int(11) NOT NULL,
  `COEType_Value` longtext,
  `RoadTaxPeriod` int(11) DEFAULT NULL,
  `LoanAmount` decimal(18,2) NOT NULL,
  `LoanTenure` int(11) NOT NULL,
  `LoanInterestRate` double NOT NULL,
  `InsurancePremium` decimal(18,2) NOT NULL,
  `NoDuoPoint` tinyint(1) NOT NULL,
  `ExemptedRoadTax` tinyint(1) NOT NULL,
  `ExemptedRegistrationFee` tinyint(1) NOT NULL,
  `ExemptedRadioLicenseFee` tinyint(1) NOT NULL,
  `AdminDiscount` decimal(18,2) NOT NULL,
  `ReturnCustomerDiscount` decimal(18,2) NOT NULL,
  `NACCustomerDiscount` decimal(18,2) NOT NULL,
  `NACNumber` longtext,
  `FleetDiscount` decimal(18,2) NOT NULL,
  `BulkCount` int(11) NOT NULL,
  `BulkDiscount` decimal(18,2) NOT NULL,
  `SaleConsultantDiscount` decimal(18,2) NOT NULL,
  `PARFRebate` decimal(18,2) NOT NULL,
  `COERebate` decimal(18,2) NOT NULL,
  `COETopUpOrBalance` decimal(18,2) NOT NULL,
  `ESTPQP` decimal(18,2) NOT NULL,
  `MinimumBidAdjustment` decimal(18,2) NOT NULL,
  `NumberRetention` decimal(18,2) NOT NULL,
  `AdminCharges` decimal(18,2) NOT NULL,
  `OtherCharges` decimal(18,2) NOT NULL,
  `ItemCharges` decimal(18,2) NOT NULL,
  `AllocationDate` datetime NOT NULL,
  `DeliveryDate` datetime NOT NULL,
  `SalesmanCommission` decimal(18,2) NOT NULL,
  `OONo` longtext,
  `OODate` datetime NOT NULL,
  `Remarks` longtext,
  `OPCDiscount` decimal(18,2) NOT NULL,
  `BookingStatus_Value` longtext,
  `BookingType_Value` longtext,
  `IsFinanceRebate` tinyint(1) NOT NULL,
  `IsInsuranceRebate` tinyint(1) NOT NULL,
  `FinanceRebate` decimal(18,2) NOT NULL,
  `InsuranceRebate` decimal(18,2) NOT NULL,
  `IsExemptedDuty` tinyint(1) NOT NULL,
  `IsExemptedARF` tinyint(1) NOT NULL,
  `Inspection` tinyint(1) NOT NULL,
  `BookType_Value` longtext,
  `RemoveClause` tinyint(1) NOT NULL,
  `NoOfVersion` int(11) NOT NULL DEFAULT '1',
  `ExportPermitNo` longtext,
  `CountryID` int(11) NOT NULL,
  `ShippingItemID` int(11) DEFAULT NULL,
  `UploadID` int(11) DEFAULT NULL,
  `CustomerID` int(11) DEFAULT NULL,
  `UserID` int(11) NOT NULL,
  `RegistrationTypeID` int(11) NOT NULL,
  `FinanceCompanyID` int(11) DEFAULT NULL,
  `InsuranceCompanyID` int(11) DEFAULT NULL,
  `IndentLineID` int(11) DEFAULT NULL,
  `AllocateID` int(11) DEFAULT NULL,
  `RegistrationID` int(11) DEFAULT NULL,
  `BookingCollectionID` int(11) DEFAULT NULL,
  `BookingCleaningID` int(11) DEFAULT NULL,
  `BookingDeliveryID` int(11) DEFAULT NULL,
  `ReservationID` int(11) DEFAULT NULL,
  `SourceOfLeadID` int(11) NOT NULL,
  `Model_Code` longtext,
  `Model_SpecCode` longtext,
  `Model_Description` longtext,
  `Model_Capacity` int(11) NOT NULL,
  `Model_ModelYear` longtext,
  `Model_CarType` longtext,
  `Model_ModelCOECategory` longtext,
  `Model_Rebate` decimal(18,2) DEFAULT NULL,
  `Model_Surcharge` decimal(18,2) DEFAULT NULL,
  `Model_CommissionSales` decimal(18,2) NOT NULL,
  `Model_CommissionDealers` decimal(18,2) NOT NULL,
  `Model_CommissionIndependent` decimal(18,2) NOT NULL,
  `Color_Code` longtext,
  `Color_Description` longtext,
  `Color_HtmlColorCode` longtext,
  `BasicPrice_RoadTax` decimal(18,2) NOT NULL,
  `BasicPrice_RoadTaxPeriod` int(11) DEFAULT NULL,
  `BasicPrice_RegistrationFee` decimal(18,2) NOT NULL,
  `BasicPrice_StartPrice` decimal(18,2) NOT NULL,
  `BasicPrice_NumberPlatePrice` decimal(18,2) NOT NULL,
  `BasicPrice_RTRebate` decimal(18,2) NOT NULL,
  `BasicPrice_RadioLicense` decimal(18,2) NOT NULL,
  `BasicPrice_Terms` longtext,
  `PriceByYear_ModelDescription` longtext,
  `PriceByYear_ModelYear` int(11) NOT NULL,
  `PriceByYear_BasicPrice` decimal(18,2) NOT NULL,
  `PriceByYear_BiddingCOE` decimal(18,2) NOT NULL,
  `PriceByYear_BiddingCOERebate` decimal(18,2) NOT NULL,
  `PriceByYear_GuaranteedCOE` decimal(18,2) NOT NULL,
  `PriceByYear_GuaranteedCOERebate` decimal(18,2) NOT NULL,
  `PriceByYear_OpenCOE` decimal(18,2) NOT NULL,
  `PriceByYear_NoCOE` decimal(18,2) NOT NULL,
  `PriceByYear_ExemptionCOE` decimal(18,2) NOT NULL,
  `_Created` datetime NOT NULL,
  `_Updated` datetime DEFAULT NULL,
  `_Deleted` datetime DEFAULT NULL,
  `EntityStatus_Value` longtext,
  `COESetting_COECategory` longtext,
  `COESetting_MaxBidTime` int(11) unsigned DEFAULT NULL,
  `MinimumDeposit` decimal(10,0) DEFAULT '0',
  `COEStatus_Value` longtext,
  `AdditionalDiscount` decimal(18,2) NOT NULL,
  `SModelDiscount` decimal(18,2) NOT NULL,
  `AdmindiscountSalesmanCommision` decimal(18,2) NOT NULL,
  `SellingPrice` decimal(18,2) NOT NULL,
  `CEVSurCharge` decimal(18,2) NOT NULL,
  `PriceByYear_BiddingCOEHigh` decimal(18,2) NOT NULL,
  `PriceByYear_GuaranteedCOEHigh` decimal(18,2) NOT NULL,
  `StandartDiscount` decimal(18,2) NOT NULL,
  `MaxBidTime` int(11) DEFAULT '0',
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID` (`ID`),
  KEY `CustomerID` (`CustomerID`),
  KEY `RegistrationTypeID` (`RegistrationTypeID`),
  KEY `FinanceCompanyID` (`FinanceCompanyID`),
  KEY `InsuranceCompanyID` (`InsuranceCompanyID`),
  KEY `UserID` (`UserID`),
  KEY `UploadID` (`UploadID`),
  KEY `IndentLineID` (`IndentLineID`),
  KEY `AllocateID` (`AllocateID`),
  KEY `ShippingItemID` (`ShippingItemID`),
  KEY `RegistrationID` (`RegistrationID`),
  KEY `BookingCollectionID` (`BookingCollectionID`),
  KEY `BookingCleaningID` (`BookingCleaningID`),
  KEY `BookingDeliveryID` (`BookingDeliveryID`),
  KEY `ReservationID` (`ReservationID`),
  KEY `SourceOfLeadID` (`SourceOfLeadID`),
  KEY `CountryID` (`CountryID`),
  CONSTRAINT `Booking_Allocate` FOREIGN KEY (`AllocateID`) REFERENCES `allocates` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `Booking_BookingCleaning` FOREIGN KEY (`BookingCleaningID`) REFERENCES `bookingcleanings` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `Booking_BookingCollection` FOREIGN KEY (`BookingCollectionID`) REFERENCES `bookingcollections` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `Booking_BookingDelivery` FOREIGN KEY (`BookingDeliveryID`) REFERENCES `bookingdeliveries` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `Booking_BookUser` FOREIGN KEY (`UserID`) REFERENCES `users` (`ID`) ON DELETE CASCADE ON UPDATE NO ACTION,
  CONSTRAINT `Booking_Country` FOREIGN KEY (`CountryID`) REFERENCES `countries` (`ID`) ON DELETE CASCADE ON UPDATE NO ACTION,
  CONSTRAINT `Booking_Customer` FOREIGN KEY (`CustomerID`) REFERENCES `customers` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `Booking_FileUpload` FOREIGN KEY (`UploadID`) REFERENCES `fileuploads` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `Booking_FinanceCompany` FOREIGN KEY (`FinanceCompanyID`) REFERENCES `financecompanies` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `Booking_IndentLine` FOREIGN KEY (`IndentLineID`) REFERENCES `indentlines` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `Booking_InsuranceCompany` FOREIGN KEY (`InsuranceCompanyID`) REFERENCES `insurancecompanies` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `Booking_Registration` FOREIGN KEY (`RegistrationID`) REFERENCES `registrations` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `Booking_RegistrationType` FOREIGN KEY (`RegistrationTypeID`) REFERENCES `registrationtypes` (`ID`) ON DELETE CASCADE ON UPDATE NO ACTION,
  CONSTRAINT `Booking_Reservation` FOREIGN KEY (`ReservationID`) REFERENCES `reservations` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `Booking_ShippingItem` FOREIGN KEY (`ShippingItemID`) REFERENCES `shippingitems` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `Booking_SourceOfLead` FOREIGN KEY (`SourceOfLeadID`) REFERENCES `sourceofleads` (`ID`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=36 DEFAULT CHARSET=utf8;


--
-- Definition of table `bookingservicepackages`
-- Service packages related to booking
--
CREATE TABLE `bookingservicepackages` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Price` decimal(18,2) NOT NULL,
  `ServicePackageID` int(11) NOT NULL,
  `Booking_ID` int(11) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID` (`ID`),
  KEY `ServicePackageID` (`ServicePackageID`),
  KEY `Booking_ID` (`Booking_ID`),
  CONSTRAINT `BookingServicePackage_ServicePackage` FOREIGN KEY (`ServicePackageID`) REFERENCES `servicepackages` (`ID`) ON DELETE CASCADE ON UPDATE NO ACTION,
  CONSTRAINT `Booking_BookingServicePackages` FOREIGN KEY (`Booking_ID`) REFERENCES `bookings` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;


--
-- Definition of table `bookingsettings`
-- Settings for booking
--
CREATE TABLE `bookingsettings` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `BookFromDate` datetime NOT NULL,
  `BookToDate` datetime NOT NULL,
  `LowRound` int(11) NOT NULL,
  `HighRound` int(11) NOT NULL,
  `Guaranteed` int(11) DEFAULT '0',
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID` (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;


--
-- Definition of table `bookingstandarditems`
-- standard items related to booking
--
CREATE TABLE `bookingstandarditems` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Price` decimal(18,2) NOT NULL,
  `OptOutDiscount` decimal(18,2) NOT NULL,
  `StandardItemID` int(11) NOT NULL,
  `Booking_ID` int(11) DEFAULT NULL,
  `Booking_ID1` int(11) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID` (`ID`),
  KEY `StandardItemID` (`StandardItemID`),
  KEY `Booking_ID` (`Booking_ID`),
  KEY `Booking_ID1` (`Booking_ID1`),
  CONSTRAINT `BookingStandardItem_StandardItem` FOREIGN KEY (`StandardItemID`) REFERENCES `standarditems` (`ID`) ON DELETE CASCADE ON UPDATE NO ACTION,
  CONSTRAINT `Booking_BookingStandardItems` FOREIGN KEY (`Booking_ID`) REFERENCES `bookings` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `Booking_OutOptStandardItems` FOREIGN KEY (`Booking_ID1`) REFERENCES `bookings` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=82 DEFAULT CHARSET=utf8;

--
-- Definition of table `bpvds`
-- deduct info and commission info for each bank payment voucher
CREATE TABLE `bpvds` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `No` longtext,
  `Commission` decimal(18,2) NOT NULL,
  `Deduct` decimal(18,2) NOT NULL,
  `OCommission` decimal(18,2) NOT NULL,
  `InvoiceID` int(11) NOT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID` (`ID`),
  KEY `InvoiceID` (`InvoiceID`),
  CONSTRAINT `BPVD_Invoice` FOREIGN KEY (`InvoiceID`) REFERENCES `invoices` (`ID`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;


--
-- Definition of table `bpvs`
-- bank payment voucher info
CREATE TABLE `bpvs` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `No` longtext,
  `Date` datetime NOT NULL,
  `Amount` decimal(18,2) NOT NULL,
  `Month` int(11) NOT NULL,
  `Year` int(11) NOT NULL,
  `Unit` int(11) NOT NULL,
  `ODeduct` decimal(18,2) NOT NULL,
  `Remark` longtext,
  `SalesmanID` int(11) NOT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID` (`ID`),
  KEY `SalesmanID` (`SalesmanID`),
  CONSTRAINT `BPV_Salesman` FOREIGN KEY (`SalesmanID`) REFERENCES `users` (`ID`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;


--
-- Definition of table `branches`
-- Branches info
CREATE TABLE `branches` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Code` longtext,
  `Name` longtext,
  `CountryID` int(11) NOT NULL,
  `IsDealerBranch` tinyint(1) NOT NULL DEFAULT '0',
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID` (`ID`),
  KEY `CountryID` (`CountryID`),
  CONSTRAINT `Branch_Country` FOREIGN KEY (`CountryID`) REFERENCES `countries` (`ID`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8;


--
-- Definition of table `carcategories`
-- Vehicle category master file (eg: FORESTER, IMPREZA,...)
--
CREATE TABLE `carcategories` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Code` longtext,
  `FuelCost` decimal(18,2) NOT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;


--
-- Definition of table `carcolors`
-- Vehicle colour master file
--
CREATE TABLE `carcolors` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Code` longtext,
  `Description` longtext,
  `HtmlColorCode` longtext,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID` (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8;


--
-- Definition of table `carmodels`
-- Vehicle model master file
--
CREATE TABLE `carmodels` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Code` longtext,
  `SpecCode` longtext,
  `Picture` longtext,
  `ProductCode` longtext,
  `Description` longtext,
  `Capacity` int(11) NOT NULL,
  `SettingPrice` decimal(18,2) NOT NULL,
  `ModelYear` longtext,
  `ModelStatus_Value` longtext,
  `COECategory_Value` longtext,
  `CommissionSales` decimal(18,2) NOT NULL,
  `CommissionDealers` decimal(18,2) NOT NULL,
  `CommissionIndependent` decimal(18,2) NOT NULL,
  `TypeID` int(11) NOT NULL,
  `ModelCategoryID` int(11) DEFAULT NULL,
  `WtsID` int(11) DEFAULT NULL,
  `CevID` int(11) DEFAULT NULL,
  `VitasID` int(11) DEFAULT NULL,
  `EnginePowerID` int(11) DEFAULT NULL,
  `_Created` datetime NOT NULL,
  `_Updated` datetime DEFAULT NULL,
  `_Deleted` datetime DEFAULT NULL,
  `EntityStatus_Value` longtext,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID` (`ID`),
  KEY `TypeID` (`TypeID`),
  KEY `ModelCategoryID` (`ModelCategoryID`),
  KEY `WtsID` (`WtsID`),
  KEY `CevID` (`CevID`),
  KEY `VitasID` (`VitasID`),
  KEY `EnginePowerID` (`EnginePowerID`),
  CONSTRAINT `CarModel_CEV` FOREIGN KEY (`CevID`) REFERENCES `modelcevs` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `CarModel_EnginePower` FOREIGN KEY (`EnginePowerID`) REFERENCES `modelenginepowers` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `CarModel_ModelCategory` FOREIGN KEY (`ModelCategoryID`) REFERENCES `modelcategories` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `CarModel_Type` FOREIGN KEY (`TypeID`) REFERENCES `cartypes` (`ID`) ON DELETE CASCADE ON UPDATE NO ACTION,
  CONSTRAINT `CarModel_Vitas` FOREIGN KEY (`VitasID`) REFERENCES `modelvitas` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `CarModel_WTS` FOREIGN KEY (`WtsID`) REFERENCES `modelwts` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8;


--
-- Definition of table `cartypes`
-- type of vehicle (eg: COMMERCIAL, PASSENGER)
--
CREATE TABLE `cartypes` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Code` longtext,
  `Description` longtext,
  `NeedDuty` tinyint(1) NOT NULL,
  `FinanceRebate` decimal(18,2) NOT NULL,
  `InsuranceRebate` decimal(18,2) NOT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID` (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;


--
-- Definition of table `changebookingcolorinfoes`
--
CREATE TABLE `changebookingcolorinfoes` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `ChangeInfo` longtext,
  `DateOfChange` datetime NOT NULL,
  `Remark` longtext,
  `FilePath` longtext,
  `BookingID` int(11) NOT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID` (`ID`),
  KEY `BookingID` (`BookingID`),
  CONSTRAINT `ChangeBookingColorInfo_Booking` FOREIGN KEY (`BookingID`) REFERENCES `bookings` (`ID`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;


--
-- Definition of table `cheqnames`
-- Salesman's cheque name
CREATE TABLE `cheqnames` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Name` longtext,
  `SalesmanID` int(11) NOT NULL,
  `BranchID` int(11) NOT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID` (`ID`),
  KEY `SalesmanID` (`SalesmanID`),
  KEY `BranchID` (`BranchID`),
  CONSTRAINT `CheqName_Branch` FOREIGN KEY (`BranchID`) REFERENCES `branches` (`ID`) ON DELETE CASCADE ON UPDATE NO ACTION,
  CONSTRAINT `CheqName_Salesman` FOREIGN KEY (`SalesmanID`) REFERENCES `users` (`ID`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;


--
-- Definition of table `classitems`
--
CREATE TABLE `classitems` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Code` longtext,
  `Description` longtext,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID` (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;


--
-- Definition of table `cleanings`
-- generate slots for cleaning
--
CREATE TABLE `cleanings` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Date` datetime NOT NULL,
  `Holiday` longtext,
  `LocationID` int(11) DEFAULT NULL,
  `A1` int(11) NOT NULL,
  `B1` int(11) NOT NULL,
  `A2` int(11) NOT NULL,
  `B2` int(11) NOT NULL,
  `A3` int(11) NOT NULL,
  `B3` int(11) NOT NULL,
  `A4` int(11) NOT NULL,
  `B4` int(11) NOT NULL,
  `A5` int(11) NOT NULL,
  `B5` int(11) NOT NULL,
  `A6` int(11) NOT NULL,
  `B6` int(11) NOT NULL,
  `A7` int(11) NOT NULL,
  `B7` int(11) NOT NULL,
  `TotalA` int(11) NOT NULL,
  `TotalB` int(11) NOT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID` (`ID`),
  KEY `LocationID` (`LocationID`),
  CONSTRAINT `Cleaning_Location` FOREIGN KEY (`LocationID`) REFERENCES `locations` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;


--
-- Definition of table `closebids`
-- Store the information of close bid( we have close bid and open bid)
--
CREATE TABLE `closebids` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Month` int(11) NOT NULL,
  `Year` int(11) NOT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID` (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

--
-- Definition of table `co2band`
-- CO2 emission band
--
CREATE TABLE `co2band` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Band` longtext,
  `CO2From` decimal(18,2) NOT NULL,
  `CO2To` decimal(18,2) NOT NULL,
  `Rebate` decimal(18,2) NOT NULL,
  `Surcharge` decimal(18,2) NOT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID` (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;


--
-- Definition of table `coebids`
-- store the bid information
CREATE TABLE `coebids` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Percent` decimal(18,2) NOT NULL,
  `Premium` decimal(18,2) NOT NULL,
  `Result_Value` longtext,
  `COENumber` longtext,
  `ExpireDate` datetime NOT NULL,
  `BookingID` int(11) NOT NULL,
  `_Created` datetime NOT NULL,
  `_Updated` datetime DEFAULT NULL,
  `_Deleted` datetime DEFAULT NULL,
  `EntityStatus_Value` longtext,
  `Month` int(11) NOT NULL DEFAULT '0',
  `Year` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID` (`ID`),
  KEY `BookingID` (`BookingID`),
  CONSTRAINT `COEBid_Booking` FOREIGN KEY (`BookingID`) REFERENCES `bookings` (`ID`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8;


--
-- Definition of table `coebidserials`
-- Store the information that manager will generate bid serials
CREATE TABLE `coebidserials` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `SerialNumber` int(11) NOT NULL,
  `BidTime` int(11) NOT NULL,
  `BiddingDate` datetime NOT NULL,
  `Amount` decimal(18,2) NOT NULL,
  `COECategory_Value` longtext,
  `IsRebid` tinyint(1) NOT NULL,
  `BookingID` int(11) NOT NULL,
  `Batch` int(11) NOT NULL,
  `Round` int(11) NOT NULL DEFAULT '1',
  `SubmittedAmount` decimal(18,2) DEFAULT '0.00',
  `TopUpAmount` decimal(18,2) DEFAULT '0.00',
  `RebateLevel` decimal(18,2) DEFAULT '0.00',
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID` (`ID`),
  KEY `BookingID` (`BookingID`),
  CONSTRAINT `COEBidSerial_Booking` FOREIGN KEY (`BookingID`) REFERENCES `bookings` (`ID`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8;


--
-- Definition of table `coebidstats`
-- statistic of coe bidding
--
CREATE TABLE `coebidstats` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `BiddingDate` datetime NOT NULL,
  `FirstBidCount` int(11) NOT NULL,
  `SencondBidCount` int(11) NOT NULL,
  `BatchCount` int(11) NOT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID` (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;


--
-- Definition of table `coebooks`
-- COE open information related to booking 
--
CREATE TABLE `coebooks` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `COECategory_Value` longtext,
  `Amount` decimal(18,2) NOT NULL,
  `AllocatedDate` datetime NOT NULL,
  `AssignedDate` datetime NOT NULL,
  `BookingID` int(11) NOT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID` (`ID`),
  KEY `BookingID` (`BookingID`),
  CONSTRAINT `COEBook_Booking` FOREIGN KEY (`BookingID`) REFERENCES `bookings` (`ID`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;


--
-- Definition of table `coedeposits`
-- deposit for COE
--
CREATE TABLE `coedeposits` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Amount` decimal(18,2) NOT NULL,
  `GST` decimal(18,2) NOT NULL,
  `BookingID` int(11) DEFAULT NULL,
  `PaymentID` int(11) NOT NULL,
  `COEDepositStatus_Value` longtext,
  `Status_Value` longtext,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID` (`ID`),
  KEY `BookingID` (`BookingID`),
  KEY `PaymentID` (`PaymentID`),
  CONSTRAINT `COEDeposit_Booking` FOREIGN KEY (`BookingID`) REFERENCES `bookings` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `COEDeposit_Payment` FOREIGN KEY (`PaymentID`) REFERENCES `payments` (`ID`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=30 DEFAULT CHARSET=utf8;

--
-- Definition of table `coeinventories`
-- Store the COE inventory information
--
CREATE TABLE `coeinventories` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Category_Value` longtext,
  `QuantityOnHand` int(11) NOT NULL,
  `QuantityAllocated` int(11) NOT NULL,
  `QuantityPending` int(11) NOT NULL,
  `Amount` decimal(18,2) NOT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID` (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;


--
-- Definition of table `coemonths`
-- Store the current bid month/year
CREATE TABLE `coemonths` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Month` int(11) NOT NULL,
  `Year` int(11) NOT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID` (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;


--
-- Definition of table `coesettings`
--
CREATE TABLE `coesettings` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Category_Value` longtext,
  `MaxBidTime` int(11) NOT NULL,
  `FinanceRebate` decimal(18,2) NOT NULL,
  `InsuranceRebate` decimal(18,2) NOT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID` (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;

--
-- Definition of table `coesubmissions`
-- Store the COE submission information

DROP TABLE IF EXISTS `coesubmissions`;
CREATE TABLE `coesubmissions` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `COECategory_Value` longtext,
  `BiddingDate` datetime NOT NULL,
  `Amount` decimal(18,2) NOT NULL,
  `Result_Value` longtext,
  `Month` int(11) NOT NULL,
  `Year` int(11) NOT NULL,
  `COEBid_ID` int(11) DEFAULT NULL,
  `SubmittedAmount` decimal(18,2) DEFAULT '0.00',
  `TopUpAmount` decimal(18,2) DEFAULT '0.00',
  `RebateLevel` decimal(18,2) DEFAULT '0.00',
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID` (`ID`),
  KEY `COEBid_ID` (`COEBid_ID`),
  CONSTRAINT `COEBid_Submissions` FOREIGN KEY (`COEBid_ID`) REFERENCES `coebids` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8;

--
-- Definition of table `collections`
-- generate slots for collection
--
DROP TABLE IF EXISTS `collections`;
CREATE TABLE `collections` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Date` datetime NOT NULL,
  `Holiday` longtext,
  `LocationID` int(11) DEFAULT NULL,
  `A1` int(11) NOT NULL,
  `B1` int(11) NOT NULL,
  `A2` int(11) NOT NULL,
  `B2` int(11) NOT NULL,
  `A3` int(11) NOT NULL,
  `B3` int(11) NOT NULL,
  `A4` int(11) NOT NULL,
  `B4` int(11) NOT NULL,
  `A5` int(11) NOT NULL,
  `B5` int(11) NOT NULL,
  `A6` int(11) NOT NULL,
  `B6` int(11) NOT NULL,
  `A7` int(11) NOT NULL,
  `B7` int(11) NOT NULL,
  `TotalA` int(11) NOT NULL,
  `TotalB` int(11) NOT NULL,
  `Confirm` longtext,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID` (`ID`),
  KEY `LocationID` (`LocationID`),
  CONSTRAINT `Collection_Location` FOREIGN KEY (`LocationID`) REFERENCES `locations` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=366 DEFAULT CHARSET=utf8;

--
-- Definition of table `confirmdocumentlines`
-- To save indent lines of LC info
--
CREATE TABLE `confirmdocumentlines` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `IndentLineId` int(11) NOT NULL,
  `Quantity` int(11) NOT NULL,
  `ConfirmDocument_ID` int(11) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID` (`ID`),
  KEY `IndentLineId` (`IndentLineId`),
  KEY `ConfirmDocument_ID` (`ConfirmDocument_ID`),
  CONSTRAINT `ConfirmDocumentLine_IndentLine` FOREIGN KEY (`IndentLineId`) REFERENCES `indentlines` (`ID`) ON DELETE CASCADE ON UPDATE NO ACTION,
  CONSTRAINT `ConfirmDocument_ConfirmDocumentLines` FOREIGN KEY (`ConfirmDocument_ID`) REFERENCES `confirmdocuments` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=30 DEFAULT CHARSET=utf8;

--
-- Definition of table `confirmdocuments`
-- To save LC info
--
CREATE TABLE `confirmdocuments` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `DenyKey` longtext,
  `LcNumber` longtext,
  `InvoiceNumber` longtext,
  `InvoiceDate` datetime NOT NULL,
  `Deadline` datetime NOT NULL,
  `RequestUserID` int(11) NOT NULL,
  `OpenLCUserID` int(11) DEFAULT NULL,
  `DenyUserID` int(11) DEFAULT NULL,
  `StatusRecordID` int(11) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID` (`ID`),
  KEY `DenyUserID` (`DenyUserID`),
  KEY `StatusRecordID` (`StatusRecordID`),
  CONSTRAINT `ConfirmDocument_DenyUser` FOREIGN KEY (`DenyUserID`) REFERENCES `users` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `ConfirmDocument_StatusRecord` FOREIGN KEY (`StatusRecordID`) REFERENCES `confirmdocumentstatus` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;

--
-- Definition of table `consolidate`
-- To save indent consolidates info
--
CREATE TABLE `consolidate` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `ConfirmReferenceNo` longtext,
  `ProinformInvoiceNo` longtext,
  `ConfirmDate` datetime NOT NULL,
  `DenyKey` longtext,
  `DenyUserID` int(11) DEFAULT NULL,
  `ApproveUserID` int(11) DEFAULT NULL,
  `UserID` int(11) NOT NULL,
  `StatusRecordID` int(11) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID` (`ID`),
  KEY `DenyUserID` (`DenyUserID`),
  KEY `ApproveUserID` (`ApproveUserID`),
  KEY `StatusRecordID` (`StatusRecordID`),
  KEY `UserID` (`UserID`),
  CONSTRAINT `Consolidate_ApproveUser` FOREIGN KEY (`ApproveUserID`) REFERENCES `users` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `Consolidate_DenyUser` FOREIGN KEY (`DenyUserID`) REFERENCES `users` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `Consolidate_StatusRecord` FOREIGN KEY (`StatusRecordID`) REFERENCES `consolidatestatus` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `Consolidate_User` FOREIGN KEY (`UserID`) REFERENCES `users` (`ID`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8;


--
-- Definition of table `counters`
-- Counter for invoice number, booking number.....
--
CREATE TABLE `counters` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Name` longtext,
  `Value` int(11) NOT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID` (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=15 DEFAULT CHARSET=utf8;


--
-- Definition of table `credits`
-- Credit infor related to invoice 
--
CREATE TABLE `credits` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `CreditNo` longtext,
  `CreditDate` datetime NOT NULL,
  `GST` decimal(18,2) NOT NULL,
  `AmountBeforeGST` decimal(18,2) NOT NULL,
  `AmountAfterGST` decimal(18,2) NOT NULL,
  `Remark` longtext,
  `CreditBy_Value` longtext,
  `CreditType_Value` longtext,
  `InvoiceID` int(11) DEFAULT NULL,
  `FcInvoiceID` int(11) DEFAULT NULL,
  `CustomerID` int(11) DEFAULT NULL,
  `FinanceID` int(11) DEFAULT NULL,
  `PaymentID` int(11) NOT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID` (`ID`),
  KEY `InvoiceID` (`InvoiceID`),
  KEY `FcInvoiceID` (`FcInvoiceID`),
  KEY `CustomerID` (`CustomerID`),
  KEY `FinanceID` (`FinanceID`),
  KEY `PaymentID` (`PaymentID`),
  CONSTRAINT `Credit_Customer` FOREIGN KEY (`CustomerID`) REFERENCES `customers` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `Credit_FcInvoice` FOREIGN KEY (`FcInvoiceID`) REFERENCES `fcinvoices` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `Credit_Finance` FOREIGN KEY (`FinanceID`) REFERENCES `financecompanies` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `Credit_Invoice` FOREIGN KEY (`InvoiceID`) REFERENCES `invoices` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `Credit_Payment` FOREIGN KEY (`PaymentID`) REFERENCES `payments` (`ID`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;

--
-- Definition of table `customers`
-- To save customers info
--
CREATE TABLE `customers` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Code` longtext,
  `NRIC` longtext,
  `Title_Value` longtext,
  `Name` longtext,
  `Address` longtext,
  `PostalCode` longtext,
  `PhoneNumber` longtext,
  `OfficeNumber` longtext,
  `MobileNumber` longtext,
  `FaxNumber` longtext,
  `Email` longtext,
  `ReferencePerson` longtext,
  `NotCall` tinyint(1) NOT NULL,
  `DateOfBirth` datetime DEFAULT NULL,
  `OccupationID` int(11) DEFAULT NULL,
  `CustomerType_Value` longtext,
  `TypeOfIndustry_Value` longtext,
  `PersonalIncomeLevel_Value` longtext,
  `MaritalStatus_Value` longtext,
  `MainUserOfCar_Value` longtext,
  `Gender_Value` longtext,
  `CurrentVehicleMake` longtext,
  `CurrentVehicleModel` longtext,
  `CurrentVehicleNo` longtext,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID` (`ID`),
  KEY `OccupationID` (`OccupationID`),
  CONSTRAINT `Customer_Occupation` FOREIGN KEY (`OccupationID`) REFERENCES `occupations` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8;

--
-- Definition of table `dbpvs`
-- Deduct old info bank payment voucher
CREATE TABLE `dbpvs` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `No` longtext,
  `Date` datetime NOT NULL,
  `Amount` decimal(18,2) NOT NULL,
  `Month` int(11) NOT NULL,
  `Year` int(11) NOT NULL,
  `Unit` int(11) NOT NULL,
  `ODeduct` decimal(18,2) NOT NULL,
  `Remark` longtext,
  `DDate` datetime NOT NULL,
  `SalesmanID` int(11) NOT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID` (`ID`),
  KEY `SalesmanID` (`SalesmanID`),
  CONSTRAINT `DBPV_Salesman` FOREIGN KEY (`SalesmanID`) REFERENCES `users` (`ID`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Definition of table `debits`
-- Debid information related to invoice
--
CREATE TABLE `debits` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `DebitNo` longtext,
  `DebitDate` datetime NOT NULL,
  `GST` decimal(18,2) NOT NULL,
  `AmountBeforeGST` decimal(18,2) NOT NULL,
  `AmountAfterGST` decimal(18,2) NOT NULL,
  `Remark` longtext,
  `DebitBy_Value` longtext,
  `DebitType_Value` longtext,
  `InvoiceID` int(11) DEFAULT NULL,
  `FcInvoiceID` int(11) DEFAULT NULL,
  `CustomerID` int(11) DEFAULT NULL,
  `FinanceID` int(11) DEFAULT NULL,
  `PaymentID` int(11) NOT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID` (`ID`),
  KEY `InvoiceID` (`InvoiceID`),
  KEY `FcInvoiceID` (`FcInvoiceID`),
  KEY `CustomerID` (`CustomerID`),
  KEY `FinanceID` (`FinanceID`),
  KEY `PaymentID` (`PaymentID`),
  CONSTRAINT `Debit_Customer` FOREIGN KEY (`CustomerID`) REFERENCES `customers` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `Debit_FcInvoice` FOREIGN KEY (`FcInvoiceID`) REFERENCES `fcinvoices` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `Debit_Finance` FOREIGN KEY (`FinanceID`) REFERENCES `financecompanies` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `Debit_Invoice` FOREIGN KEY (`InvoiceID`) REFERENCES `invoices` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `Debit_Payment` FOREIGN KEY (`PaymentID`) REFERENCES `payments` (`ID`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

--
-- Definition of table `deductibles`
-- deductibles amount for each booking
CREATE TABLE `deductibles` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Description` longtext,
  `Amount` decimal(18,2) NOT NULL,
  `BookingID` int(11) NOT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID` (`ID`),
  KEY `BookingID` (`BookingID`),
  CONSTRAINT `Deductible_Booking` FOREIGN KEY (`BookingID`) REFERENCES `bookings` (`ID`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Definition of table `delayallocations`
-- Key in delay infor when salesman want to delay allocation
--
CREATE TABLE `delayallocations` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `BookingID` int(11) NOT NULL,
  `DelayDate` datetime NOT NULL,
  `UserID` int(11) NOT NULL,
  `_Created` datetime NOT NULL,
  `_Updated` datetime DEFAULT NULL,
  `_Deleted` datetime DEFAULT NULL,
  `EntityStatus_Value` longtext,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID` (`ID`),
  KEY `BookingID` (`BookingID`),
  KEY `UserID` (`UserID`),
  CONSTRAINT `DelayAllocation_Booking` FOREIGN KEY (`BookingID`) REFERENCES `bookings` (`ID`) ON DELETE CASCADE ON UPDATE NO ACTION,
  CONSTRAINT `DelayAllocation_User` FOREIGN KEY (`UserID`) REFERENCES `users` (`ID`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;


--
-- Definition of table `deletedpayments`
-- Store the payment info that has been deleted
--
CREATE TABLE `deletedpayments` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Amount` decimal(18,2) NOT NULL,
  `UsedAmount` decimal(18,2) NOT NULL,
  `DepositNo` longtext,
  `Remark` longtext,
  `BookingPaymentType_Value` longtext,
  `BookingPaymentStatus_Value` longtext,
  `PaymentDate` datetime NOT NULL,
  `CustomerID` int(11) DEFAULT NULL,
  `DeleteUserID` int(11) NOT NULL,
  `BookingID` int(11) NOT NULL,
  `_Created` datetime NOT NULL,
  `_Updated` datetime DEFAULT NULL,
  `_Deleted` datetime DEFAULT NULL,
  `EntityStatus_Value` longtext,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID` (`ID`),
  KEY `CustomerID` (`CustomerID`),
  KEY `DeleteUserID` (`DeleteUserID`),
  KEY `BookingID` (`BookingID`),
  CONSTRAINT `DeletedPayment_Booking` FOREIGN KEY (`BookingID`) REFERENCES `bookings` (`ID`) ON DELETE CASCADE ON UPDATE NO ACTION,
  CONSTRAINT `DeletedPayment_Customer` FOREIGN KEY (`CustomerID`) REFERENCES `customers` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `DeletedPayment_DeleteUser` FOREIGN KEY (`DeleteUserID`) REFERENCES `users` (`ID`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;

--
-- Definition of table `dutypayments`
-- Duty payment info related to each vehicle
--
CREATE TABLE `dutypayments` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `DPNo` longtext,
  `DPDate` datetime NOT NULL,
  `DPAmount` decimal(18,2) NOT NULL,
  `CIF` decimal(18,2) NOT NULL,
  `ARF` decimal(18,2) NOT NULL,
  `EZNo` longtext,
  `EzDate` datetime NOT NULL,
  `OMV` decimal(18,2) NOT NULL,
  `GST` decimal(18,2) NOT NULL,
  `Status` int(11) NOT NULL,
  `DutyConfirmDate` datetime NOT NULL,
  `_Created` datetime NOT NULL,
  `_Updated` datetime DEFAULT NULL,
  `_Deleted` datetime DEFAULT NULL,
  `EntityStatus_Value` longtext,
  `Rate` decimal(18,9) NOT NULL,
  `CIFCustom` decimal(18,2) NOT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID` (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8;


--
-- Definition of table `emailreceivesettings`
-- Setting the receiver for each email template
--

CREATE TABLE `emailreceivesettings` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Type` longtext,
  `TemplateName` longtext,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID` (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=25 DEFAULT CHARSET=utf8;

--
-- Definition of table `fcinvoices`
-- To save finance company invoice info
--
CREATE TABLE `fcinvoices` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `FCInvoiceNo` longtext,
  `Amount` decimal(18,2) NOT NULL,
  `PaymentAmount` decimal(18,2) NOT NULL,
  `CompanyCommision` decimal(18,2) NOT NULL,
  `SalesmanCommision` decimal(18,2) NOT NULL,
  `CompanyIncentive` decimal(18,2) NOT NULL,
  `SalesmanIncentive` decimal(18,2) NOT NULL,
  `CompanyCommisionPercent` decimal(18,2) NOT NULL,
  `SalesmanCommisionPercent` decimal(18,2) NOT NULL,
  `TaxInvoiceDate` datetime NOT NULL,
  `GST` decimal(18,2) NOT NULL,
  `InvoiceID` int(11) NOT NULL,
  `SalesmanID` int(11) NOT NULL,
  `FinanceID` int(11) NOT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID` (`ID`),
  KEY `InvoiceID` (`InvoiceID`),
  KEY `SalesmanID` (`SalesmanID`),
  KEY `FinanceID` (`FinanceID`),
  CONSTRAINT `FCInvoice_Finance` FOREIGN KEY (`FinanceID`) REFERENCES `financecompanies` (`ID`) ON DELETE CASCADE ON UPDATE NO ACTION,
  CONSTRAINT `FCInvoice_Invoice` FOREIGN KEY (`InvoiceID`) REFERENCES `invoices` (`ID`) ON DELETE CASCADE ON UPDATE NO ACTION,
  CONSTRAINT `FCInvoice_User` FOREIGN KEY (`SalesmanID`) REFERENCES `users` (`ID`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Definition of table `fileuploads`
-- Info of file that has been upload to the system
--
CREATE TABLE `fileuploads` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `ModelName` longtext,
  `EntryID` longtext,
  `DocumentTypeID` int(11) NOT NULL,
  `ReferenceNo` longtext,
  `FileName` longtext,
  `OriginalFileName` longtext,
  `FileExtension` longtext,
  `FileSize` longtext,
  `IncludeModel` longtext,
  `IncludeID` longtext,
  `EditIndentDocument_ID` int(11) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID` (`ID`),
  KEY `DocumentTypeID` (`DocumentTypeID`),
  KEY `EditIndentDocument_ID` (`EditIndentDocument_ID`),
  CONSTRAINT `EditIndentDocument_FileUploads` FOREIGN KEY (`EditIndentDocument_ID`) REFERENCES `editindentdocuments` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FileUpload_DocumentType` FOREIGN KEY (`DocumentTypeID`) REFERENCES `documenttypes` (`ID`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=18 DEFAULT CHARSET=utf8;

--
-- Definition of table `financecommissions`
--
CREATE TABLE `financecommissions` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `FromDate` datetime NOT NULL,
  `ToDate` datetime NOT NULL,
  `Interest` double NOT NULL,
  `MinLoad` decimal(18,2) NOT NULL,
  `SalesmanType` longtext,
  `CarTypeID` int(11) NOT NULL,
  `FinanceCompanyID` int(11) NOT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID` (`ID`),
  KEY `CarTypeID` (`CarTypeID`),
  KEY `FinanceCompanyID` (`FinanceCompanyID`),
  CONSTRAINT `FinanceCommission_CarType` FOREIGN KEY (`CarTypeID`) REFERENCES `cartypes` (`ID`) ON DELETE CASCADE ON UPDATE NO ACTION,
  CONSTRAINT `FinanceCommission_FinanceCompany` FOREIGN KEY (`FinanceCompanyID`) REFERENCES `financecompanies` (`ID`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Definition of table `financecompanies`
--
CREATE TABLE `financecompanies` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Code` longtext,
  `Name` longtext,
  `Address` longtext,
  `PostalCode` longtext,
  `PhoneNumber` longtext,
  `FaxNumber` longtext,
  `ReferenceContact` longtext,
  `Email` longtext,
  `_Created` datetime NOT NULL,
  `_Updated` datetime DEFAULT NULL,
  `_Deleted` datetime DEFAULT NULL,
  `EntityStatus_Value` longtext,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID` (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

--
-- Definition of table `financeincentives`
--
CREATE TABLE `financeincentives` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `FromDate` datetime NOT NULL,
  `ToDate` datetime NOT NULL,
  `SalesmanType` longtext,
  `CarTypeID` int(11) NOT NULL,
  `FinanceCompanyID` int(11) NOT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID` (`ID`),
  KEY `CarTypeID` (`CarTypeID`),
  KEY `FinanceCompanyID` (`FinanceCompanyID`),
  CONSTRAINT `FinanceIncentive_CarType` FOREIGN KEY (`CarTypeID`) REFERENCES `cartypes` (`ID`) ON DELETE CASCADE ON UPDATE NO ACTION,
  CONSTRAINT `FinanceIncentive_FinanceCompany` FOREIGN KEY (`FinanceCompanyID`) REFERENCES `financecompanies` (`ID`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Definition of table `generatecheques`
-- generate cheque info for invoice refund
CREATE TABLE `generatecheques` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `GenerateNo` int(11) NOT NULL,
  `BankCode` longtext,
  `Date` datetime NOT NULL,
  `PayeeName` longtext,
  `RefNo` longtext,
  `Amount` decimal(18,2) NOT NULL,
  `ChequeType_Value` longtext,
  `UserID` int(11) DEFAULT NULL,
  `DDate` datetime DEFAULT NULL,
  `Status_Value` longtext,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Definition of table `holidays`
-- Holiday master file
CREATE TABLE `holidays` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Date` datetime NOT NULL,
  `Description` longtext,
  `HolidayType` longtext,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID` (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

--
-- Definition of table `ifcommissions`
-- Store the commission info for finance
--
CREATE TABLE `ifcommissions` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `FinanceAmount` decimal(18,2) NOT NULL,
  `Commcp` decimal(18,2) NOT NULL,
  `Commc` decimal(18,2) NOT NULL,
  `Commsp` decimal(18,2) NOT NULL,
  `Comms` decimal(18,2) NOT NULL,
  `Incec` decimal(18,2) NOT NULL,
  `Inces` decimal(18,2) NOT NULL,
  `Tino` longtext,
  `TiDate` datetime NOT NULL,
  `PDate` datetime NOT NULL,
  `PAmount` decimal(18,2) NOT NULL,
  `GST` decimal(18,2) NOT NULL,
  `InvoiceID` int(11) NOT NULL,
  `SalesmanID` int(11) NOT NULL,
  `FinanceID` int(11) NOT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID` (`ID`),
  KEY `InvoiceID` (`InvoiceID`),
  KEY `SalesmanID` (`SalesmanID`),
  KEY `FinanceID` (`FinanceID`),
  CONSTRAINT `IFCommission_Finance` FOREIGN KEY (`FinanceID`) REFERENCES `financecompanies` (`ID`) ON DELETE CASCADE ON UPDATE NO ACTION,
  CONSTRAINT `IFCommission_Invoice` FOREIGN KEY (`InvoiceID`) REFERENCES `invoices` (`ID`) ON DELETE CASCADE ON UPDATE NO ACTION,
  CONSTRAINT `IFCommission_Salesman` FOREIGN KEY (`SalesmanID`) REFERENCES `users` (`ID`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Definition of table `indentcosts`
-- Store the cost related to indent
--
CREATE TABLE `indentcosts` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Description` longtext,
  `Pricing` decimal(18,2) NOT NULL,
  `IndentID` int(11) NOT NULL,
  `ExchangeID` int(11) NOT NULL,
  `FileUpload` longtext,
  `IndentHistory_ID` int(11) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID` (`ID`),
  KEY `IndentID` (`IndentID`),
  KEY `ExchangeID` (`ExchangeID`),
  KEY `IndentHistory_ID` (`IndentHistory_ID`),
  CONSTRAINT `IndentCost_Exchange` FOREIGN KEY (`ExchangeID`) REFERENCES `exchanges` (`ID`) ON DELETE CASCADE ON UPDATE NO ACTION,
  CONSTRAINT `IndentCost_Indent` FOREIGN KEY (`IndentID`) REFERENCES `indents` (`ID`) ON DELETE CASCADE ON UPDATE NO ACTION,
  CONSTRAINT `IndentHistory_IndentCosts` FOREIGN KEY (`IndentHistory_ID`) REFERENCES `indenthistories` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;

--
-- Definition of table `indentlines`
-- To save indent lines info
--
CREATE TABLE `indentlines` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `ModelID` int(11) NOT NULL,
  `ColorID` int(11) NOT NULL,
  `Quantity` int(11) NOT NULL,
  `ManufactureQuantity` int(11) NOT NULL,
  `AvailableQuantity` int(11) NOT NULL,
  `LCRemainQuantity` int(11) NOT NULL DEFAULT '0',
  `ShippedQuantity` int(11) NOT NULL,
  `ShippedConfirmQuantity` int(11) NOT NULL,
  `BookedQuantity` int(11) NOT NULL,
  `IndentID` int(11) NOT NULL,
  `CIFPerUnit` decimal(18,2) NOT NULL,
  `CIF` decimal(18,2) NOT NULL,
  `ManufactureCIF` decimal(18,2) NOT NULL,
  `Year` int(11) NOT NULL,
  `IndentHistory_ID` int(11) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID` (`ID`),
  KEY `ModelID` (`ModelID`),
  KEY `ColorID` (`ColorID`),
  KEY `IndentID` (`IndentID`),
  KEY `IndentHistory_ID` (`IndentHistory_ID`),
  CONSTRAINT `IndentHistory_IndentLines` FOREIGN KEY (`IndentHistory_ID`) REFERENCES `indenthistories` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `IndentLine_Color` FOREIGN KEY (`ColorID`) REFERENCES `carcolors` (`ID`) ON DELETE CASCADE ON UPDATE NO ACTION,
  CONSTRAINT `IndentLine_Indent` FOREIGN KEY (`IndentID`) REFERENCES `indents` (`ID`) ON DELETE CASCADE ON UPDATE NO ACTION,
  CONSTRAINT `IndentLine_Model` FOREIGN KEY (`ModelID`) REFERENCES `carmodels` (`ID`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=44 DEFAULT CHARSET=utf8;

--
-- Definition of table `indents`
-- To save indents info
--
CREATE TABLE `indents` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `IndentNo` longtext,
  `Date` datetime NOT NULL,
  `EstimateArrivalDate` datetime NOT NULL,
  `DueDate` datetime NOT NULL,
  `SupplierID` int(11) NOT NULL,
  `ExchangeID` int(11) NOT NULL,
  `CountryID` int(11) NOT NULL,
  `StatusRecordID` int(11) DEFAULT NULL,
  `ConsolidateID` int(11) DEFAULT NULL,
  `DenyKey` longtext,
  `DenyUserID` int(11) DEFAULT NULL,
  `ConfirmUserID` int(11) DEFAULT NULL,
  `IsRequested` tinyint(1) NOT NULL,
  `ManufacturerOrderNo` longtext,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID` (`ID`),
  KEY `DenyUserID` (`DenyUserID`),
  KEY `ConfirmUserID` (`ConfirmUserID`),
  KEY `SupplierID` (`SupplierID`),
  KEY `CountryID` (`CountryID`),
  KEY `ExchangeID` (`ExchangeID`),
  KEY `StatusRecordID` (`StatusRecordID`),
  KEY `ConsolidateID` (`ConsolidateID`),
  CONSTRAINT `Consolidate_Indents` FOREIGN KEY (`ConsolidateID`) REFERENCES `consolidate` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `Indent_ConfirmUser` FOREIGN KEY (`ConfirmUserID`) REFERENCES `users` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `Indent_Country` FOREIGN KEY (`CountryID`) REFERENCES `countries` (`ID`) ON DELETE CASCADE ON UPDATE NO ACTION,
  CONSTRAINT `Indent_DenyUser` FOREIGN KEY (`DenyUserID`) REFERENCES `users` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `Indent_Exchange` FOREIGN KEY (`ExchangeID`) REFERENCES `exchanges` (`ID`) ON DELETE CASCADE ON UPDATE NO ACTION,
  CONSTRAINT `Indent_StatusRecord` FOREIGN KEY (`StatusRecordID`) REFERENCES `indentstatus` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `Indent_Supplier` FOREIGN KEY (`SupplierID`) REFERENCES `suppliers` (`ID`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8;

--
-- Definition of table `insurancecompanies`
-- Master file of insurance company
--
CREATE TABLE `insurancecompanies` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Code` longtext,
  `Name` longtext,
  `Address` longtext,
  `PostalCode` longtext,
  `PhoneNumber` longtext,
  `FaxNumber` longtext,
  `ReferenceContact` longtext,
  `Email` longtext,
  `_Created` datetime NOT NULL,
  `_Updated` datetime DEFAULT NULL,
  `_Deleted` datetime DEFAULT NULL,
  `EntityStatus_Value` longtext,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID` (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

--
-- Definition of table `insurances`
-- Insurance company master file
--
CREATE TABLE `insurances` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Code` longtext,
  `Name` longtext,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Definition of table `inventories`
-- To save inventories info (by model, color, year, country)
--
CREATE TABLE `inventories` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `ModelID` int(11) NOT NULL,
  `ColorID` int(11) NOT NULL,
  `Year` int(11) NOT NULL,
  `CountryID` int(11) NOT NULL,
  `TotalQuantity` int(11) NOT NULL,
  `OrderQuantity` int(11) NOT NULL,
  `OHQuantity` int(11) NOT NULL,
  `IndentBookingQuantity` int(11) NOT NULL,
  `ExstockBookingQuantity` int(11) NOT NULL,
  `SoldQuantity` int(11) NOT NULL,
  `ReservationQuantity` int(11) NOT NULL,
  `_Created` datetime NOT NULL,
  `_Updated` datetime DEFAULT NULL,
  `_Deleted` datetime DEFAULT NULL,
  `EntityStatus_Value` longtext,
  `Type_Value` varchar(10) DEFAULT '2',
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID` (`ID`),
  KEY `CountryID` (`CountryID`),
  KEY `ModelID` (`ModelID`),
  KEY `ColorID` (`ColorID`),
  CONSTRAINT `Inventory_Color` FOREIGN KEY (`ColorID`) REFERENCES `carcolors` (`ID`) ON DELETE CASCADE ON UPDATE NO ACTION,
  CONSTRAINT `Inventory_Country` FOREIGN KEY (`CountryID`) REFERENCES `countries` (`ID`) ON DELETE CASCADE ON UPDATE NO ACTION,
  CONSTRAINT `Inventory_Model` FOREIGN KEY (`ModelID`) REFERENCES `carmodels` (`ID`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=25 DEFAULT CHARSET=utf8;

--
-- Definition of table `invoices`
-- To save invoices info
--
CREATE TABLE `invoices` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `InvoiceNo` longtext,
  `InvoiceDate` datetime NOT NULL,
  `InvoiceAmount` decimal(18,2) NOT NULL,
  `InvoiceCost` decimal(18,2) NOT NULL,
  `AccessoriesAmount` decimal(18,2) NOT NULL,
  `AccessoriesCost` decimal(18,2) NOT NULL,
  `CustomerDue` decimal(18,2) NOT NULL,
  `CustomerPay` decimal(18,2) NOT NULL,
  `InsuranceAmount` int(11) NOT NULL,
  `FinanceDue` decimal(18,2) NOT NULL,
  `FinancePay` decimal(18,2) NOT NULL,
  `GSTAmount` decimal(18,2) NOT NULL,
  `IsIncludeGST` tinyint(1) NOT NULL,
  `TaxAmount` decimal(18,2) NOT NULL,
  `IsBPV` tinyint(1) NOT NULL,
  `BookingID` int(11) NOT NULL,
  `_Created` datetime NOT NULL,
  `_Updated` datetime DEFAULT NULL,
  `_Deleted` datetime DEFAULT NULL,
  `EntityStatus_Value` longtext,
  `InsurancePremium` decimal(18,2) NOT NULL,
  `LoanAmount` decimal(18,2) NOT NULL,
  `LoanTenure` int(11) NOT NULL,
  `LoanInterestRate` double NOT NULL,
  `RoadTax` decimal(18,2) NOT NULL,
  `RoadTaxPeriod` int(11) DEFAULT NULL,
  `RegistrationFee` decimal(18,2) NOT NULL,
  `NumberPlatePrice` decimal(18,2) NOT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID` (`ID`),
  KEY `BookingID` (`BookingID`),
  CONSTRAINT `Invoice_Booking` FOREIGN KEY (`BookingID`) REFERENCES `bookings` (`ID`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

--
-- Definition of table `iudels`
-- Save info for faulty IU
--
CREATE TABLE `iudels` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Code` longtext,
  `Batch` longtext,
  `Date` datetime NOT NULL,
  `Remark` longtext,
  `UserID` int(11) NOT NULL,
  `_Created` datetime NOT NULL,
  `_Updated` datetime DEFAULT NULL,
  `_Deleted` datetime DEFAULT NULL,
  `EntityStatus_Value` longtext,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID` (`ID`),
  KEY `UserID` (`UserID`),
  CONSTRAINT `IUDel_User` FOREIGN KEY (`UserID`) REFERENCES `users` (`ID`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

--
-- Definition of table `iureplaces`
-- Info commissioned and decommissioned IU when IU was assigned for vehicle
--
CREATE TABLE `iureplaces` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `ChassisNo` longtext,
  `OldCode` longtext,
  `OldBatch` longtext,
  `OldADate` datetime NOT NULL,
  `OldCDate` datetime NOT NULL,
  `NewCode` longtext,
  `NewBatch` longtext,
  `NewADate` datetime NOT NULL,
  `Remarks` longtext,
  `UpdateDate` datetime NOT NULL,
  `UserID` int(11) NOT NULL,
  `NewCDate` datetime DEFAULT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID` (`ID`),
  KEY `UserID` (`UserID`),
  CONSTRAINT `IUReplace_User` FOREIGN KEY (`UserID`) REFERENCES `users` (`ID`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

--
-- Definition of table `ius`
-- IU info
--
CREATE TABLE `ius` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Code` longtext,
  `Batch` longtext,
  `Date` datetime NOT NULL,
  `CDate` datetime DEFAULT NULL,
  `SealNo` longtext,
  `IUStatus_Value` longtext,
  `_Created` datetime NOT NULL,
  `_Updated` datetime DEFAULT NULL,
  `_Deleted` datetime DEFAULT NULL,
  `EntityStatus_Value` longtext,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID` (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=utf8;

--
-- Definition of table `languages`
--
CREATE TABLE `languages` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Value` longtext,
  `DisplayName` longtext,
  `Image` longtext,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID` (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;

--
-- Definition of table `lcopens`
-- To save Open LC info
--
CREATE TABLE `lcopens` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `ApplicationDate` datetime NOT NULL,
  `LCDate` datetime NOT NULL,
  `LCNumber` longtext NOT NULL,
  `PaymentDeadline` datetime DEFAULT NULL,
  `PaymentToBankDate` datetime DEFAULT NULL,
  `OpenUserID` int(11) DEFAULT NULL,
  `PaidUserID` int(11) DEFAULT NULL,
  `ConfirmDocmentID` int(11) NOT NULL,
  `StatusRecordID` int(11) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID` (`ID`),
  KEY `PaidUserID` (`PaidUserID`),
  KEY `OpenUserID` (`OpenUserID`),
  KEY `ConfirmDocmentID` (`ConfirmDocmentID`),
  KEY `StatusRecordID` (`StatusRecordID`),
  CONSTRAINT `LCOpen_ConfirmDocument` FOREIGN KEY (`ConfirmDocmentID`) REFERENCES `confirmdocuments` (`ID`) ON DELETE CASCADE ON UPDATE NO ACTION,
  CONSTRAINT `LCOpen_OpenUser` FOREIGN KEY (`OpenUserID`) REFERENCES `users` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `LCOpen_PaidUser` FOREIGN KEY (`PaidUserID`) REFERENCES `users` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `LCOpen_StatusRecord` FOREIGN KEY (`StatusRecordID`) REFERENCES `confirmdocumentstatus` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;

--
-- Definition of table `locationhistories`
-- Keep tract of location of each vehicle
CREATE TABLE `locationhistories` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `CreatedDate` datetime NOT NULL,
  `ShippingItemID` int(11) NOT NULL,
  `LocationID` int(11) NOT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID` (`ID`) USING BTREE,
  KEY `ShippingItemID` (`ShippingItemID`) USING BTREE,
  KEY `LocationID` (`LocationID`) USING BTREE,
  CONSTRAINT `locationhistories_ibfk_1` FOREIGN KEY (`LocationID`) REFERENCES `locations` (`ID`) ON DELETE CASCADE ON UPDATE NO ACTION,
  CONSTRAINT `locationhistories_ibfk_2` FOREIGN KEY (`ShippingItemID`) REFERENCES `shippingitems` (`ID`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=35 DEFAULT CHARSET=latin1 ROW_FORMAT=COMPACT;

--
-- Definition of table `locations`
-- Master file of location
--
CREATE TABLE `locations` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Code` longtext,
  `Description` longtext,
  `IsActive` tinyint(1) NOT NULL,
  `Bonded_Value` varchar(50) NOT NULL DEFAULT '0',
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID` (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;

--
-- Definition of table `ltapayments`
-- Store the info of payment for LTA
--
CREATE TABLE `ltapayments` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `BPVNo` longtext,
  `Date` datetime NOT NULL,
  `Payto` longtext,
  `Model` longtext,
  `Engine` longtext,
  `Chassis` longtext,
  `TotalAmount` decimal(18,2) NOT NULL,
  `ARF` decimal(18,2) NOT NULL,
  `RoadTax` decimal(18,2) NOT NULL,
  `RegistrationFee` decimal(18,2) NOT NULL,
  `TransferFee` decimal(18,2) NOT NULL,
  `PARF` decimal(18,2) NOT NULL,
  `COERebate` decimal(18,2) NOT NULL,
  `Hackney` decimal(18,2) NOT NULL,
  `COEPremium` decimal(18,2) NOT NULL,
  `COEDeposit` decimal(18,2) NOT NULL,
  `COEBalance` decimal(18,2) NOT NULL,
  `Others` decimal(18,2) NOT NULL,
  `TCPARF` decimal(18,2) NOT NULL,
  `TCCOERebate` decimal(18,2) NOT NULL,
  `PaidAmount` decimal(18,2) NOT NULL,
  `PaidDate` datetime NOT NULL,
  `COEType_Value` longtext,
  `InvoiceID` int(11) NOT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID` (`ID`),
  KEY `InvoiceID` (`InvoiceID`),
  CONSTRAINT `LTAPayment_Invoice` FOREIGN KEY (`InvoiceID`) REFERENCES `invoices` (`ID`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Definition of table `matrixdiscounts`
-- Setting for maximum, minimum discount for salesman and manager related to booking
-- Use to booking
--
CREATE TABLE `matrixdiscounts` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `UserID` int(11) NOT NULL,
  `DiscountFrom` decimal(18,2) NOT NULL,
  `DiscountEnd` decimal(18,2) NOT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID` (`ID`),
  KEY `UserID` (`UserID`),
  CONSTRAINT `MatrixDiscount_User` FOREIGN KEY (`UserID`) REFERENCES `users` (`ID`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;


--
-- Definition of table `modelbasicprices`
-- Basic price related to each model
--
CREATE TABLE `modelbasicprices` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `RoadTax` decimal(18,2) NOT NULL,
  `RoadTaxPeriod` int(11) NOT NULL,
  `RegistrationFee` decimal(18,2) NOT NULL,
  `StartPrice` decimal(18,2) NOT NULL,
  `NumberPlatePrice` decimal(18,2) NOT NULL,
  `RTRebate` decimal(18,2) NOT NULL,
  `Terms` longtext,
  `DisplayOnWeb` tinyint(1) NOT NULL,
  `RadioLicense` decimal(18,2) NOT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID` (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=29 DEFAULT CHARSET=utf8;

--
-- Definition of table `modelcategories`
-- Model ca
--
CREATE TABLE `modelcategories` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Name` longtext,
  `Category` longtext,
  `FuelCost` decimal(18,2) NOT NULL,
  `Status` int(11) NOT NULL,
  `IsActive` tinyint(1) NOT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID` (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8;

--
-- Definition of table `modelcevs`
-- CEV information related to model
--
DROP TABLE IF EXISTS `modelcevs`;
CREATE TABLE `modelcevs` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `CO2` longtext,
  `Band` longtext,
  `Rebate` decimal(18,2) NOT NULL,
  `Surcharge` decimal(18,2) NOT NULL,
  `_Created` datetime NOT NULL,
  `_Updated` datetime DEFAULT NULL,
  `_Deleted` datetime DEFAULT NULL,
  `EntityStatus_Value` longtext,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID` (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8;

--
-- Definition of table `modelcommissions`
-- Commission for booker related to each model
--
CREATE TABLE `modelcommissions` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `ModelYear` int(11) NOT NULL,
  `CommissionSalesman` decimal(18,2) NOT NULL,
  `CommissionIsc` decimal(18,2) NOT NULL,
  `Domestic` tinyint(1) NOT NULL,
  `ModelID` int(11) NOT NULL,
  `_Created` datetime NOT NULL,
  `_Updated` datetime DEFAULT NULL,
  `_Deleted` datetime DEFAULT NULL,
  `EntityStatus_Value` longtext,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID` (`ID`),
  KEY `ModelID` (`ModelID`),
  CONSTRAINT `ModelCommission_Model` FOREIGN KEY (`ModelID`) REFERENCES `carmodels` (`ID`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Definition of table `modeldiscounts`
-- Discount for booking, related to each model
--
CREATE TABLE `modeldiscounts` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `ModelID` int(11) NOT NULL,
  `ModelCode` longtext,
  `ModelSpecCode` longtext,
  `ModelColor` longtext,
  `ModelYear` int(11) NOT NULL,
  `COECategory_Value` longtext,
  `Description` longtext,
  `DiscountValue` decimal(18,2) NOT NULL,
  `Remarks` longtext,
  `StartTime` datetime NOT NULL,
  `EndTime` datetime NOT NULL,
  `CreatedBy` longtext,
  `_Created` datetime NOT NULL,
  `_Updated` datetime DEFAULT NULL,
  `_Deleted` datetime DEFAULT NULL,
  `EntityStatus_Value` longtext,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID` (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;

--
-- Definition of table `modelenginepowers`
-- Engine, power value related to each model
CREATE TABLE `modelenginepowers` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Description` longtext,
  `EPType_Value` longtext,
  `Value` int(11) NOT NULL,
  `_Created` datetime NOT NULL,
  `_Updated` datetime DEFAULT NULL,
  `_Deleted` datetime DEFAULT NULL,
  `EntityStatus_Value` longtext,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID` (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8;

--
-- Definition of table `modeloptionalitems`
-- Optional item related to each model
CREATE TABLE `modeloptionalitems` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Price` decimal(18,2) NOT NULL,
  `ShowInContract` tinyint(1) NOT NULL,
  `OptionalItemID` int(11) NOT NULL,
  `CarModel_ID` int(11) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID` (`ID`),
  KEY `OptionalItemID` (`OptionalItemID`),
  KEY `CarModel_ID` (`CarModel_ID`),
  CONSTRAINT `CarModel_ModelOptionalItems` FOREIGN KEY (`CarModel_ID`) REFERENCES `carmodels` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `ModelOptionalItem_OptionalItem` FOREIGN KEY (`OptionalItemID`) REFERENCES `optionalitems` (`ID`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=24 DEFAULT CHARSET=utf8;

--
-- Definition of table `modelpricebyyears`
-- Price value for each model by year
--
CREATE TABLE `modelpricebyyears` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `ModelDescription` longtext,
  `ModelYear` int(11) NOT NULL,
  `BasicPrice` decimal(18,2) NOT NULL,
  `BiddingCOE` decimal(18,2) NOT NULL,
  `BiddingCOERebate` decimal(18,2) NOT NULL,
  `GuaranteedCOE` decimal(18,2) NOT NULL,
  `GuaranteedCOERebate` decimal(18,2) NOT NULL,
  `OpenCOE` decimal(18,2) NOT NULL,
  `NoCOE` decimal(18,2) NOT NULL,
  `ExemptionCOE` decimal(18,2) NOT NULL,
  `IsDefaultPrice` tinyint(1) NOT NULL,
  `_Created` datetime NOT NULL,
  `_Updated` datetime DEFAULT NULL,
  `_Deleted` datetime DEFAULT NULL,
  `EntityStatus_Value` longtext,
  `ModelPrice_ID` int(11) DEFAULT NULL,
  `BiddingCOEHigh` decimal(18,2) NOT NULL,
  `GuaranteedCOEHigh` decimal(18,2) NOT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID` (`ID`),
  KEY `ModelPrice_ID` (`ModelPrice_ID`),
  CONSTRAINT `ModelPrice_PriceByYears` FOREIGN KEY (`ModelPrice_ID`) REFERENCES `modelprices` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=87 DEFAULT CHARSET=utf8;

--
-- Definition of table `modelpriceschedules`
-- Store the price value to auto update the price by scheduler
CREATE TABLE `modelpriceschedules` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `EffectiveDate` datetime NOT NULL,
  `IsActive` tinyint(1) NOT NULL,
  `Values` longtext,
  `Remarks` longtext,
  `CreatedBy` longtext,
  `COECategory_Value` longtext,
  `ModelID` int(11) DEFAULT NULL,
  `ModelYear` int(11) NOT NULL,
  `_Created` datetime NOT NULL,
  `_Updated` datetime DEFAULT NULL,
  `_Deleted` datetime DEFAULT NULL,
  `EntityStatus_Value` longtext,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID` (`ID`),
  KEY `ModelID` (`ModelID`),
  CONSTRAINT `ModelPriceSchedule_Model` FOREIGN KEY (`ModelID`) REFERENCES `carmodels` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;

--
-- Definition of table `modelpromotionitems`
-- Promotion item related to each model
--
CREATE TABLE `modelpromotionitems` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Price` decimal(18,2) NOT NULL,
  `ShowInContract` tinyint(1) NOT NULL,
  `PromotionItemID` int(11) NOT NULL,
  `CarModel_ID` int(11) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID` (`ID`),
  KEY `PromotionItemID` (`PromotionItemID`),
  KEY `CarModel_ID` (`CarModel_ID`),
  CONSTRAINT `CarModel_ModelPromotionItems` FOREIGN KEY (`CarModel_ID`) REFERENCES `carmodels` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `ModelPromotionItem_PromotionItem` FOREIGN KEY (`PromotionItemID`) REFERENCES `promotionitems` (`ID`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8;

--
-- Definition of table `models_colors`
-- store which color belong to which model
--
CREATE TABLE `models_colors` (
  `Model_Id` int(11) NOT NULL,
  `Color_Id` int(11) NOT NULL,
  PRIMARY KEY (`Model_Id`,`Color_Id`),
  KEY `CarModel_Colors_Target` (`Color_Id`),
  CONSTRAINT `CarModel_Colors_Source` FOREIGN KEY (`Model_Id`) REFERENCES `carmodels` (`ID`) ON DELETE CASCADE ON UPDATE NO ACTION,
  CONSTRAINT `CarModel_Colors_Target` FOREIGN KEY (`Color_Id`) REFERENCES `carcolors` (`ID`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Definition of table `modelservicepackages`
-- Service packages related to model
--
CREATE TABLE `modelservicepackages` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Price` decimal(18,2) NOT NULL,
  `ServicePackageID` int(11) NOT NULL,
  `CarModel_ID` int(11) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID` (`ID`),
  KEY `ServicePackageID` (`ServicePackageID`),
  KEY `CarModel_ID` (`CarModel_ID`),
  CONSTRAINT `CarModel_ModelServicePackages` FOREIGN KEY (`CarModel_ID`) REFERENCES `carmodels` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `ModelServicePackage_ServicePackage` FOREIGN KEY (`ServicePackageID`) REFERENCES `servicepackages` (`ID`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Definition of table `modelstandarditems`
-- Standart item related to each model
--
CREATE TABLE `modelstandarditems` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Price` decimal(18,2) NOT NULL,
  `Discount` decimal(18,2) NOT NULL,
  `NotOutOpt` tinyint(1) NOT NULL,
  `ShowInContract` tinyint(1) NOT NULL,
  `StandardItemID` int(11) NOT NULL,
  `CarModel_ID` int(11) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID` (`ID`),
  KEY `StandardItemID` (`StandardItemID`),
  KEY `CarModel_ID` (`CarModel_ID`),
  CONSTRAINT `CarModel_ModelStandardItems` FOREIGN KEY (`CarModel_ID`) REFERENCES `carmodels` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `ModelStandardItem_StandardItem` FOREIGN KEY (`StandardItemID`) REFERENCES `standarditems` (`ID`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=24 DEFAULT CHARSET=utf8;

--
-- Definition of table `modelvitas`
-- Vitas informaton related to each model
--
CREATE TABLE `modelvitas` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Code` longtext,
  `AttachID` int(11) NOT NULL,
  `_Created` datetime NOT NULL,
  `_Updated` datetime DEFAULT NULL,
  `_Deleted` datetime DEFAULT NULL,
  `EntityStatus_Value` longtext,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID` (`ID`),
  KEY `AttachID` (`AttachID`),
  CONSTRAINT `ModelVitas_Attach` FOREIGN KEY (`AttachID`) REFERENCES `attachitems` (`ID`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8;

--
-- Definition of table `modelwts`
-- WTS information related to each model
CREATE TABLE `modelwts` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `UmKgs` double NOT NULL,
  `MlwKgs` double NOT NULL,
  `FrontTyre` longtext,
  `RearTyre` longtext,
  `FrontSeat` longtext,
  `RearSeat` longtext,
  `_Created` datetime NOT NULL,
  `_Updated` datetime DEFAULT NULL,
  `_Deleted` datetime DEFAULT NULL,
  `EntityStatus_Value` longtext,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID` (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

--
-- Definition of table `opencoes`
-- Store the Open COE information
--
CREATE TABLE `opencoes` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `BidMonth` int(11) NOT NULL,
  `BidYear` int(11) NOT NULL,
  `ExpiryDate` datetime NOT NULL,
  `Category_Value` longtext,
  `Premium` decimal(18,2) NOT NULL,
  `COENo` longtext,
  `Status_Value` longtext,
  `BookingID` int(11) DEFAULT NULL,
  `RefNo` int(11) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID` (`ID`),
  KEY `BookingID` (`BookingID`),
  CONSTRAINT `OpenCOE_Booking` FOREIGN KEY (`BookingID`) REFERENCES `bookings` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=51 DEFAULT CHARSET=utf8;

--
-- Definition of table `payments`
-- Payment information related to booking or invoice
--
CREATE TABLE `payments` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `PaymentNo` longtext,
  `Amount` decimal(18,2) NOT NULL,
  `DisbursementDate` datetime NOT NULL,
  `PaymentType_Value` longtext,
  `PaymentBy_Value` longtext,
  `PaymentFor_Value` longtext,
  `PaymentDate` datetime NOT NULL,
  `PaymentStatus_Value` longtext,
  `CustomerID` int(11) DEFAULT NULL,
  `FinancialID` int(11) DEFAULT NULL,
  `BookingID` int(11) DEFAULT NULL,
  `InvoiceID` int(11) DEFAULT NULL,
  `FCInvoiceID` int(11) DEFAULT NULL,
  `Remark` longtext,
  `TransferNumber` longtext,
  `ChequeNumber` longtext,
  `PaymentPayee_Value` longtext,
  `CreditCardNo` longtext,
  `ParfcoeNumber` longtext,
  `BankTransferNo` longtext,
  `Voucher` longtext,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID` (`ID`),
  KEY `CustomerID` (`CustomerID`),
  KEY `FinancialID` (`FinancialID`),
  KEY `BookingID` (`BookingID`),
  KEY `InvoiceID` (`InvoiceID`),
  KEY `FCInvoiceID` (`FCInvoiceID`),
  CONSTRAINT `FCInvoice_Payments` FOREIGN KEY (`FCInvoiceID`) REFERENCES `fcinvoices` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `Payment_Booking` FOREIGN KEY (`BookingID`) REFERENCES `bookings` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `Payment_Customer` FOREIGN KEY (`CustomerID`) REFERENCES `customers` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `Payment_FinanceCompany` FOREIGN KEY (`FinancialID`) REFERENCES `financecompanies` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `Payment_Invoice` FOREIGN KEY (`InvoiceID`) REFERENCES `invoices` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=33 DEFAULT CHARSET=utf8;

--
-- Definition of table `prospectcomments`
-- Comment for prospect between salesman and manager
--
CREATE TABLE `prospectcomments` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Comment` longtext,
  `DateTime` datetime NOT NULL,
  `UserID` int(11) NOT NULL,
  `Prospect_ID` int(11) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID` (`ID`),
  KEY `UserID` (`UserID`),
  KEY `Prospect_ID` (`Prospect_ID`),
  CONSTRAINT `ProspectComment_User` FOREIGN KEY (`UserID`) REFERENCES `users` (`ID`) ON DELETE CASCADE ON UPDATE NO ACTION,
  CONSTRAINT `Prospect_ProspectComments` FOREIGN KEY (`Prospect_ID`) REFERENCES `prospects` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Definition of table `prospects`
-- To save prospects info
--
CREATE TABLE `prospects` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `CustomerID` int(11) DEFAULT NULL,
  `Model_ModelID` int(11) NOT NULL,
  `Model_Code` longtext,
  `Model_SpecCode` longtext,
  `Model_Description` longtext,
  `Model_Capacity` int(11) NOT NULL,
  `Model_ModelYear` longtext,
  `Category_Value` longtext,
  `BoughtVehicleMake` longtext,
  `BoughtVehicleModel` longtext,
  `SalesmanID` int(11) DEFAULT NULL,
  `NCD` int(11) NOT NULL,
  `PassDate` datetime DEFAULT NULL,
  `Remark` longtext,
  `KeyInDate` datetime NOT NULL,
  `ExpiryDate` datetime NOT NULL,
  `HaveTestDrive` tinyint(1) NOT NULL,
  `SourceOfLeadID` int(11) NOT NULL,
  `TDVehicleID` int(11) DEFAULT NULL,
  `TradePlateID` int(11) DEFAULT NULL,
  `_Created` datetime NOT NULL,
  `_Updated` datetime DEFAULT NULL,
  `_Deleted` datetime DEFAULT NULL,
  `EntityStatus_Value` longtext,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID` (`ID`),
  KEY `SalesmanID` (`SalesmanID`),
  KEY `CustomerID` (`CustomerID`),
  KEY `SourceOfLeadID` (`SourceOfLeadID`),
  KEY `TDVehicleID` (`TDVehicleID`),
  KEY `TradePlateID` (`TradePlateID`),
  CONSTRAINT `Prospect_Customer` FOREIGN KEY (`CustomerID`) REFERENCES `customers` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `Prospect_Salesman` FOREIGN KEY (`SalesmanID`) REFERENCES `users` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `Prospect_SourceOfLead` FOREIGN KEY (`SourceOfLeadID`) REFERENCES `sourceofleads` (`ID`) ON DELETE CASCADE ON UPDATE NO ACTION,
  CONSTRAINT `Prospect_TdVehicle` FOREIGN KEY (`TDVehicleID`) REFERENCES `tdvehicles` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `Prospect_TradePlate` FOREIGN KEY (`TradePlateID`) REFERENCES `tradeplates` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;
--
-- Definition of table `quotations`
-- Store the quotatin info
--
CREATE TABLE `quotations` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `ContractNumber` longtext,
  `QuotationDate` datetime NOT NULL,
  `SSSC` tinyint(1) NOT NULL,
  `RegisteredAddress` longtext,
  `CarModelID` int(11) NOT NULL,
  `CarColorID` int(11) NOT NULL,
  `COEType_Value` longtext,
  `RoadTaxPeriod` int(11) DEFAULT NULL,
  `LoanAmount` decimal(18,2) NOT NULL,
  `LoanTenure` int(11) NOT NULL,
  `LoanInterestRate` double NOT NULL,
  `InsurancePremium` decimal(18,2) NOT NULL,
  `NoDuoPoint` tinyint(1) NOT NULL,
  `ExemptedRoadTax` tinyint(1) NOT NULL,
  `ExemptedRegistrationFee` tinyint(1) NOT NULL,
  `ExemptedRadioLicenseFee` tinyint(1) NOT NULL,
  `AdminDiscount` decimal(18,2) NOT NULL,
  `ReturnCustomerDiscount` decimal(18,2) NOT NULL,
  `NACCustomerDiscount` decimal(18,2) NOT NULL,
  `NACNumber` longtext,
  `FleetDiscount` decimal(18,2) NOT NULL,
  `BulkCount` int(11) NOT NULL,
  `BulkDiscount` decimal(18,2) NOT NULL,
  `SaleConsultantDiscount` decimal(18,2) NOT NULL,
  `PARFRebate` decimal(18,2) NOT NULL,
  `COERebate` decimal(18,2) NOT NULL,
  `COETopUpOrBalance` decimal(18,2) NOT NULL,
  `ESTPQP` decimal(18,2) NOT NULL,
  `MinimumBidAdjustment` decimal(18,2) NOT NULL,
  `NumberRetention` decimal(18,2) NOT NULL,
  `AdminCharges` decimal(18,2) NOT NULL,
  `OtherCharges` decimal(18,2) NOT NULL,
  `ItemCharges` decimal(18,2) NOT NULL,
  `AllocationDate` datetime NOT NULL,
  `DeliveryDate` datetime NOT NULL,
  `SalesmanCommission` decimal(18,2) NOT NULL,
  `OONo` longtext,
  `OODate` datetime NOT NULL,
  `Remarks` longtext,
  `OPCDiscount` decimal(18,2) NOT NULL,
  `BookingType_Value` longtext,
  `IsFinanceRebate` tinyint(1) NOT NULL,
  `IsInsuranceRebate` tinyint(1) NOT NULL,
  `FinanceRebate` decimal(18,2) NOT NULL,
  `InsuranceRebate` decimal(18,2) NOT NULL,
  `Duty` tinyint(1) NOT NULL,
  `ARF` tinyint(1) NOT NULL,
  `Inspection` tinyint(1) NOT NULL,
  `BookType_Value` longtext,
  `RemoveClause` tinyint(1) NOT NULL,
  `CountryID` int(11) NOT NULL,
  `ShippingItemID` int(11) DEFAULT NULL,
  `UploadID` int(11) DEFAULT NULL,
  `CustomerID` int(11) DEFAULT NULL,
  `UserID` int(11) NOT NULL,
  `RegistrationTypeID` int(11) NOT NULL,
  `FinanceCompanyID` int(11) DEFAULT NULL,
  `InsuranceCompanyID` int(11) DEFAULT NULL,
  `IndentLineID` int(11) DEFAULT NULL,
  `AllocateID` int(11) DEFAULT NULL,
  `RegistrationID` int(11) DEFAULT NULL,
  `QuotationCollectionID` int(11) DEFAULT NULL,
  `QuotationCleaningID` int(11) DEFAULT NULL,
  `QuotationDeliveryID` int(11) DEFAULT NULL,
  `ReservationID` int(11) DEFAULT NULL,
  `SourceOfLeadID` int(11) NOT NULL,
  `Model_Code` longtext,
  `Model_SpecCode` longtext,
  `Model_Description` longtext,
  `Model_Capacity` int(11) NOT NULL,
  `Model_ModelYear` longtext,
  `Model_ModelCOECategory` longtext,
  `Model_Rebate` decimal(18,2) DEFAULT NULL,
  `Model_Surcharge` decimal(18,2) DEFAULT NULL,
  `Color_Code` longtext,
  `Color_Description` longtext,
  `Color_HtmlColorCode` longtext,
  `BasicPrice_RoadTax` decimal(18,2) NOT NULL,
  `BasicPrice_RoadTaxPeriod` int(11) DEFAULT NULL,
  `BasicPrice_RegistrationFee` decimal(18,2) NOT NULL,
  `BasicPrice_StartPrice` decimal(18,2) NOT NULL,
  `BasicPrice_NumberPlatePrice` decimal(18,2) NOT NULL,
  `BasicPrice_RTRebate` decimal(18,2) NOT NULL,
  `BasicPrice_Terms` longtext,
  `PriceByYear_ModelDescription` longtext,
  `PriceByYear_ModelYear` int(11) NOT NULL,
  `PriceByYear_BasicPrice` decimal(18,2) NOT NULL,
  `PriceByYear_BiddingCOE` decimal(18,2) NOT NULL,
  `PriceByYear_BiddingCOERebate` decimal(18,2) NOT NULL,
  `PriceByYear_GuaranteedCOE` decimal(18,2) NOT NULL,
  `PriceByYear_GuaranteedCOERebate` decimal(18,2) NOT NULL,
  `PriceByYear_OpenCOE` decimal(18,2) NOT NULL,
  `PriceByYear_NoCOE` decimal(18,2) NOT NULL,
  `PriceByYear_ExemptionCOE` decimal(18,2) NOT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID` (`ID`),
  KEY `CustomerID` (`CustomerID`),
  KEY `RegistrationTypeID` (`RegistrationTypeID`),
  KEY `FinanceCompanyID` (`FinanceCompanyID`),
  KEY `InsuranceCompanyID` (`InsuranceCompanyID`),
  KEY `UserID` (`UserID`),
  KEY `UploadID` (`UploadID`),
  KEY `IndentLineID` (`IndentLineID`),
  KEY `AllocateID` (`AllocateID`),
  KEY `ShippingItemID` (`ShippingItemID`),
  KEY `RegistrationID` (`RegistrationID`),
  KEY `QuotationCollectionID` (`QuotationCollectionID`),
  KEY `QuotationCleaningID` (`QuotationCleaningID`),
  KEY `QuotationDeliveryID` (`QuotationDeliveryID`),
  KEY `ReservationID` (`ReservationID`),
  KEY `SourceOfLeadID` (`SourceOfLeadID`),
  KEY `CountryID` (`CountryID`),
  CONSTRAINT `Quotation_Allocate` FOREIGN KEY (`AllocateID`) REFERENCES `allocates` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `Quotation_BookUser` FOREIGN KEY (`UserID`) REFERENCES `users` (`ID`) ON DELETE CASCADE ON UPDATE NO ACTION,
  CONSTRAINT `Quotation_Country` FOREIGN KEY (`CountryID`) REFERENCES `countries` (`ID`) ON DELETE CASCADE ON UPDATE NO ACTION,
  CONSTRAINT `Quotation_Customer` FOREIGN KEY (`CustomerID`) REFERENCES `customers` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `Quotation_FileUpload` FOREIGN KEY (`UploadID`) REFERENCES `fileuploads` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `Quotation_FinanceCompany` FOREIGN KEY (`FinanceCompanyID`) REFERENCES `financecompanies` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `Quotation_IndentLine` FOREIGN KEY (`IndentLineID`) REFERENCES `indentlines` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `Quotation_InsuranceCompany` FOREIGN KEY (`InsuranceCompanyID`) REFERENCES `insurancecompanies` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `Quotation_QuotationCleaning` FOREIGN KEY (`QuotationCleaningID`) REFERENCES `quotationcleanings` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `Quotation_QuotationCollection` FOREIGN KEY (`QuotationCollectionID`) REFERENCES `quotationcollections` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `Quotation_QuotationDelivery` FOREIGN KEY (`QuotationDeliveryID`) REFERENCES `quotationdeliveries` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `Quotation_Registration` FOREIGN KEY (`RegistrationID`) REFERENCES `registrations` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `Quotation_RegistrationType` FOREIGN KEY (`RegistrationTypeID`) REFERENCES `registrationtypes` (`ID`) ON DELETE CASCADE ON UPDATE NO ACTION,
  CONSTRAINT `Quotation_Reservation` FOREIGN KEY (`ReservationID`) REFERENCES `reservations` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `Quotation_ShippingItem` FOREIGN KEY (`ShippingItemID`) REFERENCES `shippingitems` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `Quotation_SourceOfLead` FOREIGN KEY (`SourceOfLeadID`) REFERENCES `sourceofleads` (`ID`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;

--
-- Definition of table `rebates`
-- Store the rebate information related to booking
CREATE TABLE `rebates` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Description` longtext,
  `Amount` decimal(18,2) NOT NULL,
  `BookingID` int(11) NOT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID` (`ID`),
  KEY `BookingID` (`BookingID`),
  CONSTRAINT `Rebate_Booking` FOREIGN KEY (`BookingID`) REFERENCES `bookings` (`ID`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;
--
-- Definition of table `refunds`
-- Store the refund information related to invoice
CREATE TABLE `refunds` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `RefundNo` longtext,
  `RefundDate` datetime NOT NULL,
  `Amount` decimal(18,2) NOT NULL,
  `Remark` longtext,
  `RefundType_Value` longtext,
  `InvoiceID` int(11) DEFAULT NULL,
  `FCInvoiceID` int(11) DEFAULT NULL,
  `CreditID` int(11) DEFAULT NULL,
  `CustomerID` int(11) DEFAULT NULL,
  `FinanceID` int(11) DEFAULT NULL,
  `PaymentID` int(11) DEFAULT NULL,
  `GenerateChequeID` int(11) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID` (`ID`),
  KEY `InvoiceID` (`InvoiceID`),
  KEY `FCInvoiceID` (`FCInvoiceID`),
  KEY `CreditID` (`CreditID`),
  KEY `CustomerID` (`CustomerID`),
  KEY `FinanceID` (`FinanceID`),
  KEY `PaymentID` (`PaymentID`),
  KEY `GenerateChequeID` (`GenerateChequeID`),
  CONSTRAINT `Refund_Credit` FOREIGN KEY (`CreditID`) REFERENCES `credits` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `Refund_Customer` FOREIGN KEY (`CustomerID`) REFERENCES `customers` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `Refund_FcInvoice` FOREIGN KEY (`FCInvoiceID`) REFERENCES `fcinvoices` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `Refund_Finance` FOREIGN KEY (`FinanceID`) REFERENCES `financecompanies` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `Refund_GenerateCheque` FOREIGN KEY (`GenerateChequeID`) REFERENCES `generatecheques` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `Refund_Invoice` FOREIGN KEY (`InvoiceID`) REFERENCES `invoices` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `Refund_Payment` FOREIGN KEY (`PaymentID`) REFERENCES `payments` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Definition of table `reportschedules`
-- Store Schedule send report information
CREATE TABLE `reportschedules` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `RunTime` datetime NOT NULL,
  `EmailTo` longtext,
  `EmailCC` longtext,
  `EmailBCC` longtext,
  `Description` longtext,
  `ReportName` longtext,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Definition of table `reservations`
-- To save reservations info
--
CREATE TABLE `reservations` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `ReservationNo` int(11) NOT NULL,
  `RefNo` longtext,
  `SalesmanID` int(11) DEFAULT NULL,
  `ModelID` int(11) NOT NULL,
  `ColorID` int(11) NOT NULL,
  `Year` int(11) NOT NULL,
  `ReservationType` longtext,
  `ReservationFor_Value` longtext,
  `IndentID` int(11) DEFAULT NULL,
  `Remark` longtext,
  `Reason` longtext,
  `AllocateDate` datetime DEFAULT NULL,
  `RegistrationTypeID` int(11) NOT NULL,
  `DeleteUserID` int(11) DEFAULT NULL,
  `ShippingItemID` int(11) DEFAULT NULL,
  `StatusRecordID` int(11) DEFAULT NULL,
  `CountryID` int(11) NOT NULL,
  `Charges` decimal(18,2) DEFAULT NULL,
  `Discount` decimal(18,2) DEFAULT NULL,
  `Mileage` decimal(18,2) DEFAULT NULL,
  `RequestRemark` longtext,
  `_Created` datetime NOT NULL,
  `_Updated` datetime DEFAULT NULL,
  `_Deleted` datetime DEFAULT NULL,
  `EntityStatus_Value` longtext,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID` (`ID`),
  KEY `StatusRecordID` (`StatusRecordID`),
  KEY `ModelID` (`ModelID`),
  KEY `ColorID` (`ColorID`),
  KEY `IndentID` (`IndentID`),
  KEY `RegistrationTypeID` (`RegistrationTypeID`),
  KEY `SalesmanID` (`SalesmanID`),
  KEY `DeleteUserID` (`DeleteUserID`),
  KEY `ShippingItemID` (`ShippingItemID`),
  KEY `CountryID` (`CountryID`),
  CONSTRAINT `Reservation_Color` FOREIGN KEY (`ColorID`) REFERENCES `carcolors` (`ID`) ON DELETE CASCADE ON UPDATE NO ACTION,
  CONSTRAINT `Reservation_Country` FOREIGN KEY (`CountryID`) REFERENCES `countries` (`ID`) ON DELETE CASCADE ON UPDATE NO ACTION,
  CONSTRAINT `Reservation_DeleteUser` FOREIGN KEY (`DeleteUserID`) REFERENCES `users` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `Reservation_Indent` FOREIGN KEY (`IndentID`) REFERENCES `indents` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `Reservation_Model` FOREIGN KEY (`ModelID`) REFERENCES `carmodels` (`ID`) ON DELETE CASCADE ON UPDATE NO ACTION,
  CONSTRAINT `Reservation_RegistrationType` FOREIGN KEY (`RegistrationTypeID`) REFERENCES `registrationtypes` (`ID`) ON DELETE CASCADE ON UPDATE NO ACTION,
  CONSTRAINT `Reservation_Salesman` FOREIGN KEY (`SalesmanID`) REFERENCES `users` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `Reservation_ShippingItem` FOREIGN KEY (`ShippingItemID`) REFERENCES `shippingitems` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `Reservation_StatusRecord` FOREIGN KEY (`StatusRecordID`) REFERENCES `reservationstatusrecords` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8;

--
-- Definition of table `shippingitems`
-- To save shipping items info
--
CREATE TABLE `shippingitems` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `CaseNo` longtext,
  `ChassisNo` longtext,
  `EngineNo` longtext,
  `CIF` decimal(18,2) NOT NULL,
  `CIFSin` decimal(18,2) NOT NULL,
  `OMV` decimal(18,2) NOT NULL,
  `KeyNo` longtext,
  `IndentLineID` int(11) NOT NULL,
  `ShippingID` int(11) NOT NULL,
  `LocationID` int(11) DEFAULT NULL,
  `CountryID` int(11) NOT NULL,
  `ModelID` int(11) NOT NULL,
  `Year` int(11) NOT NULL,
  `ColorID` int(11) NOT NULL,
  `PermitID` int(11) DEFAULT NULL,
  `DutyPaymentID` int(11) DEFAULT NULL,
  `TempDutyID` int(11) DEFAULT NULL,
  `IsChoosen` int(11) NOT NULL,
  `SubmitPaymentDate` datetime NOT NULL,
  `ReadyDate` datetime DEFAULT NULL,
  `IUID` int(11) DEFAULT NULL,
  `ShippingTransfer_ID` int(11) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID` (`ID`),
  KEY `IndentLineID` (`IndentLineID`),
  KEY `ShippingID` (`ShippingID`),
  KEY `LocationID` (`LocationID`),
  KEY `PermitID` (`PermitID`),
  KEY `DutyPaymentID` (`DutyPaymentID`),
  KEY `TempDutyID` (`TempDutyID`),
  KEY `CountryID` (`CountryID`),
  KEY `IUID` (`IUID`),
  KEY `ModelID` (`ModelID`),
  KEY `ColorID` (`ColorID`),
  KEY `ShippingTransfer_ID` (`ShippingTransfer_ID`),
  CONSTRAINT `ShippingItem_Color` FOREIGN KEY (`ColorID`) REFERENCES `carcolors` (`ID`) ON DELETE CASCADE ON UPDATE NO ACTION,
  CONSTRAINT `ShippingItem_Country` FOREIGN KEY (`CountryID`) REFERENCES `countries` (`ID`) ON DELETE CASCADE ON UPDATE NO ACTION,
  CONSTRAINT `ShippingItem_DutyPayment` FOREIGN KEY (`DutyPaymentID`) REFERENCES `dutypayments` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `ShippingItem_IndentLine` FOREIGN KEY (`IndentLineID`) REFERENCES `indentlines` (`ID`) ON DELETE CASCADE ON UPDATE NO ACTION,
  CONSTRAINT `ShippingItem_IU` FOREIGN KEY (`IUID`) REFERENCES `ius` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `ShippingItem_Location` FOREIGN KEY (`LocationID`) REFERENCES `locations` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `ShippingItem_Model` FOREIGN KEY (`ModelID`) REFERENCES `carmodels` (`ID`) ON DELETE CASCADE ON UPDATE NO ACTION,
  CONSTRAINT `ShippingItem_Permit` FOREIGN KEY (`PermitID`) REFERENCES `permits` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `ShippingItem_TempDuty` FOREIGN KEY (`TempDutyID`) REFERENCES `tempduties` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `ShippingTransfer_ShippingItems` FOREIGN KEY (`ShippingTransfer_ID`) REFERENCES `shippingtransfers` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `Shipping_ShippingItems` FOREIGN KEY (`ShippingID`) REFERENCES `shippings` (`ID`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=31 DEFAULT CHARSET=utf8;

--
-- Definition of table `shippings`
-- To save shipping info
--
CREATE TABLE `shippings` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `InvoiceNo` longtext,
  `EstimatedDepartureDate` datetime NOT NULL,
  `EstimatedArrivalDate` datetime NOT NULL,
  `Arrival` datetime DEFAULT NULL,
  `DeclarationNo` longtext,
  `VesselName` longtext,
  `Quantity` int(11) NOT NULL,
  `Rate` decimal(18,9) NOT NULL,
  `SupplierID` int(11) NOT NULL,
  `CurrencyID` int(11) NOT NULL,
  `StatusRecordID` int(11) DEFAULT NULL,
  `_Created` datetime NOT NULL,
  `_Updated` datetime DEFAULT NULL,
  `_Deleted` datetime DEFAULT NULL,
  `EntityStatus_Value` longtext,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID` (`ID`),
  KEY `SupplierID` (`SupplierID`),
  KEY `CurrencyID` (`CurrencyID`),
  KEY `StatusRecordID` (`StatusRecordID`),
  CONSTRAINT `Shipping_Currency` FOREIGN KEY (`CurrencyID`) REFERENCES `currencies` (`ID`) ON DELETE CASCADE ON UPDATE NO ACTION,
  CONSTRAINT `Shipping_StatusRecord` FOREIGN KEY (`StatusRecordID`) REFERENCES `shippingstatusrecords` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `Shipping_Supplier` FOREIGN KEY (`SupplierID`) REFERENCES `suppliers` (`ID`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8;


--
-- Definition of table `shippingtransfers`
--
CREATE TABLE `shippingtransfers` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `CountryFromID` int(11) NOT NULL,
  `CountryToID` int(11) NOT NULL,
  `TransferDate` datetime NOT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID` (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;

--
-- Definition of table `sourceofleads`
--
CREATE TABLE `sourceofleads` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Description` longtext NOT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID` (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;

--
-- Definition of table `tdhistories`
-- Store the test drive history
CREATE TABLE `tdhistories` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `RegistrationNo` longtext,
  `ModelID` int(11) NOT NULL,
  `CustomerID` int(11) NOT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID` (`ID`),
  KEY `ModelID` (`ModelID`),
  KEY `CustomerID` (`CustomerID`),
  CONSTRAINT `TDHistory_Customer` FOREIGN KEY (`CustomerID`) REFERENCES `customers` (`ID`) ON DELETE CASCADE ON UPDATE NO ACTION,
  CONSTRAINT `TDHistory_Model` FOREIGN KEY (`ModelID`) REFERENCES `carmodels` (`ID`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

--
-- Definition of table `tdvehicles`
-- Store the vehicle that available for test drive
CREATE TABLE `tdvehicles` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Engine` longtext,
  `Chassis` longtext,
  `ModelID` int(11) NOT NULL,
  `BranchID` int(11) NOT NULL,
  `ReservationID` int(11) NOT NULL,
  `LocationID` int(11) NOT NULL,
  `Status_Value` longtext,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID` (`ID`),
  KEY `ModelID` (`ModelID`),
  KEY `BranchID` (`BranchID`),
  KEY `ReservationID` (`ReservationID`),
  KEY `LocationID` (`LocationID`),
  CONSTRAINT `TDVehicle_Branch` FOREIGN KEY (`BranchID`) REFERENCES `branches` (`ID`) ON DELETE CASCADE ON UPDATE NO ACTION,
  CONSTRAINT `TDVehicle_Location` FOREIGN KEY (`LocationID`) REFERENCES `locations` (`ID`) ON DELETE CASCADE ON UPDATE NO ACTION,
  CONSTRAINT `TDVehicle_Model` FOREIGN KEY (`ModelID`) REFERENCES `carmodels` (`ID`) ON DELETE CASCADE ON UPDATE NO ACTION,
  CONSTRAINT `TDVehicle_Reservation` FOREIGN KEY (`ReservationID`) REFERENCES `reservations` (`ID`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;








