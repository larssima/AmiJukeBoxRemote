CREATE TABLE `jbselection` (
  `Id` int(11) unsigned NOT NULL AUTO_INCREMENT,
  `JbLetter` varchar(1) NOT NULL,
  `JbNumberA` varchar(2) NOT NULL,
  `JbNumberB` varchar(2) DEFAULT NULL,
  `JbNumeric` int(11) NOT NULL,
  `A1Song` varchar(45) NOT NULL,
  `A2Song` varchar(45) DEFAULT NULL,
  `B1Song` varchar(45) NOT NULL,
  `B2Song` varchar(45) DEFAULT NULL,
  `Artist1` varchar(45) NOT NULL,
  `Artist2` varchar(45) DEFAULT NULL,
  `ImageStripName` varchar(255) DEFAULT NULL,
  `MusicCategory` varchar(45) DEFAULT NULL,
  `Archived` int(11) NOT NULL DEFAULT '0',
  `ImageStripTemplate` varchar(255) DEFAULT NULL,
  `DiscogsLink` varchar(1024) DEFAULT NULL,
  `SpotifyUri` varchar(1024) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=133 DEFAULT CHARSET=utf8;
SELECT * FROM amijukebox.jbselection;