CREATE TABLE `PlatformAccounts` (
   `Id` int(11) NOT NULL AUTO_INCREMENT,
   `Name` varchar(64) DEFAULT NULL,
   `Password` text,
   `Mobile` varchar(64) DEFAULT NULL,
   `Extend1` text,
   `Extend2` text,
   `Extend3` text,
   `Extend4` text,
   `Extend5` text,
   `Extend6` text,
   `Extend7` text,
   `Extend8` text,
   `Extend9` text,
   `iExtend1` int(11) NOT NULL,
   `iExtend2` int(11) NOT NULL,
   `iExtend3` int(11) NOT NULL,
   `iExtend4` int(11) NOT NULL,
   `iExtend5` int(11) NOT NULL,
   `iExtend6` int(11) NOT NULL,
   `iExtend7` int(11) NOT NULL,
   `iExtend8` int(11) NOT NULL,
   `iExtend9` int(11) NOT NULL,
   PRIMARY KEY (`Id`),
   KEY `NewIndex1` (`Name`)
 ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4


