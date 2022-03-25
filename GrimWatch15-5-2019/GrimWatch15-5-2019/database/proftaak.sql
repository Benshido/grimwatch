-- phpMyAdmin SQL Dump
-- version 4.8.3
-- https://www.phpmyadmin.net/
--
-- Host: localhost
-- Gegenereerd op: 20 mei 2019 om 09:40
-- Serverversie: 5.7.19
-- PHP-versie: 7.1.20

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `proftaak`
--

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `attack`
--

CREATE TABLE `attack` (
  `attackId` int(3) NOT NULL,
  `attackName` varchar(1000) NOT NULL,
  `minAttackDamage` int(11) NOT NULL,
  `maxAttackDamage` int(11) NOT NULL,
  `charId` int(3) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Gegevens worden geëxporteerd voor tabel `attack`
--

INSERT INTO `attack` (`attackId`, `attackName`, `minAttackDamage`, `maxAttackDamage`, `charId`) VALUES
(1, 'Sword Slice', 10, 50, 1),
(2, 'Ultimate Sword Strike: Rage', 20, 100, 1),
(3, 'Holy Hell', 40, 200, 1),
(4, 'Piercing Arrow', 10, 50, 2),
(5, 'Arrow Rain', 20, 100, 2),
(6, 'Arrow Dynamic Strike', 40, 200, 2),
(7, 'Water Break', 10, 50, 3),
(8, 'Chaos Rain', 20, 100, 3),
(9, 'Eternal Darkness', 40, 200, 3),
(10, 'Headbutt', 10, 50, 4),
(11, 'Boulder Smash', 20, 100, 4),
(12, 'Rage', 40, 200, 4),
(13, 'Ichimonji', 10, 50, 5),
(14, 'Suwari No Tori No Kamae', 20, 100, 5),
(15, 'Honor Blade', 40, 200, 5),
(16, 'Twilight Fire', 10, 50, 6),
(17, 'Blood Surge', 20, 100, 6),
(18, 'Black Magic', 40, 200, 6),
(19, 'Axe Throw', 10, 50, 7),
(20, 'Charge', 20, 100, 7),
(21, 'Air Strike', 40, 200, 7),
(22, 'Stab', 10, 50, 8),
(23, 'Deathly Fangs', 20, 100, 8),
(24, 'Venomous Bite', 40, 200, 8),
(25, 'Shadow Claw ', 10, 50, 9),
(26, 'Fireball', 20, 100, 9),
(27, 'Magma Blade', 40, 200, 9),
(28, 'Ember', 10, 50, 10),
(29, 'Pyromania', 20, 100, 10),
(30, 'Sea Of Flames', 40, 200, 10),
(31, 'Sword Strike', 10, 50, 11),
(32, 'Ultimate Sword Strike: Rage', 20, 100, 11),
(33, 'Holy Hell', 40, 200, 11),
(34, 'Bite', 20, 100, 12),
(35, 'Feast', 40, 200, 12),
(36, 'The Final Supper ', 100, 1000, 12);

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `charactercategory`
--

CREATE TABLE `charactercategory` (
  `catId` int(10) NOT NULL,
  `catName` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Gegevens worden geëxporteerd voor tabel `charactercategory`
--

INSERT INTO `charactercategory` (`catId`, `catName`) VALUES
(1, 'Hero'),
(2, 'Boss');

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `characters`
--

CREATE TABLE `characters` (
  `charId` int(3) NOT NULL,
  `charName` varchar(100) NOT NULL,
  `catId` int(3) NOT NULL,
  `charHealth` int(10) NOT NULL,
  `charDamage` int(3) NOT NULL,
  `charDefense` int(3) NOT NULL,
  `charImage` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Gegevens worden geëxporteerd voor tabel `characters`
--

INSERT INTO `characters` (`charId`, `charName`, `catId`, `charHealth`, `charDamage`, `charDefense`, `charImage`) VALUES
(1, 'Grim Hunter', 1, 250, 10, 10, 'knight'),
(2, 'Grim Hunter', 1, 250, 10, 10, 'Archer'),
(3, 'The Great Cthulhu ', 2, 1500, 10, 10, '-Cthulhu '),
(4, 'Undying Twins', 2, 700, 10, 10, '-Ogre'),
(5, 'Shinobi Kazou', 2, 1300, 10, 10, '-Samurai'),
(6, 'Henjaw, The Exiled Priestess', 2, 900, 10, 10, '-witch'),
(7, 'Areras, The Fallen Gaurdian', 2, 1000, 10, 10, '-minotaur'),
(8, 'Baishe, The ShapeShifter', 2, 500, 10, 10, '-snake'),
(9, 'Karnag, The Demon Of Carnage', 2, 1100, 10, 10, '-demon'),
(10, 'Ylris, Bringer OF Death', 2, 1200, 10, 10, '-dragon'),
(11, 'The Previous Grim hunter', 2, 1400, 10, 10, '-skeleton'),
(12, 'The Final Supper', 2, 1600, 10, 10, '-slime');

--
-- Indexen voor geëxporteerde tabellen
--

--
-- Indexen voor tabel `attack`
--
ALTER TABLE `attack`
  ADD PRIMARY KEY (`attackId`);

--
-- Indexen voor tabel `charactercategory`
--
ALTER TABLE `charactercategory`
  ADD PRIMARY KEY (`catId`);

--
-- Indexen voor tabel `characters`
--
ALTER TABLE `characters`
  ADD PRIMARY KEY (`charId`);

--
-- AUTO_INCREMENT voor geëxporteerde tabellen
--

--
-- AUTO_INCREMENT voor een tabel `attack`
--
ALTER TABLE `attack`
  MODIFY `attackId` int(3) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=37;

--
-- AUTO_INCREMENT voor een tabel `charactercategory`
--
ALTER TABLE `charactercategory`
  MODIFY `catId` int(10) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT voor een tabel `characters`
--
ALTER TABLE `characters`
  MODIFY `charId` int(3) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=13;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
