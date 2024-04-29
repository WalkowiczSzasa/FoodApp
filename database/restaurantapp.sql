-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Gép: 127.0.0.1
-- Létrehozás ideje: 2024. Ápr 30. 01:42
-- Kiszolgáló verziója: 10.4.32-MariaDB
-- PHP verzió: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Adatbázis: `restaurantapp`
--

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `allergene`
--

CREATE TABLE `allergene` (
  `allergeneID` int(11) NOT NULL,
  `allergeneName` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `customer`
--

CREATE TABLE `customer` (
  `ID` int(11) NOT NULL,
  `name` varchar(40) NOT NULL,
  `phoneNumber` varchar(30) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `deliveryfee`
--

CREATE TABLE `deliveryfee` (
  `ID` int(11) NOT NULL,
  `price` double NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

--
-- A tábla adatainak kiíratása `deliveryfee`
--

INSERT INTO `deliveryfee` (`ID`, `price`) VALUES
(1, 500);

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `destination`
--

CREATE TABLE `destination` (
  `ID` int(11) NOT NULL,
  `orderType` tinyint(1) NOT NULL COMMENT '0=Elvitel\r\n1=Kiszállítás',
  `orderStreet` varchar(50) NOT NULL,
  `orderStreetNum` varchar(11) NOT NULL,
  `customerID` int(4) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `drink`
--

CREATE TABLE `drink` (
  `ID` int(11) NOT NULL,
  `drinkName` varchar(40) NOT NULL,
  `drinkPrice` int(5) NOT NULL COMMENT 'VAT percentage of the drink f.e.:27%',
  `drinkStatus` int(1) NOT NULL COMMENT '0=Not Available\r\n1=Available'
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `finishedorders`
--

CREATE TABLE `finishedorders` (
  `forderID` int(11) NOT NULL,
  `orderNote` varchar(150) NOT NULL COMMENT 'Additional note for the order. ',
  `orderTime` datetime NOT NULL COMMENT ' 	The time when the order was taken. ',
  `orderDueTime` datetime NOT NULL COMMENT ' 	The time when the order is due. ',
  `orderDestID` int(4) NOT NULL,
  `orderDispatchID` int(3) NOT NULL COMMENT 'The ID of the delivery man if the orderType is 0 then its the ELVITEL users ID. ',
  `foodID` varchar(255) NOT NULL,
  `drinkID` varchar(255) NOT NULL,
  `paymentID` int(4) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `food`
--

CREATE TABLE `food` (
  `ID` int(11) NOT NULL,
  `foodName` varchar(50) NOT NULL,
  `foodDesc` varchar(100) NOT NULL COMMENT 'Foods description',
  `foodVAT` varchar(5) NOT NULL COMMENT 'VAt percentage of the food, f.e.: 27%.',
  `foodPrice` int(5) NOT NULL,
  `foodAllergenID` varchar(40) NOT NULL COMMENT 'This field contains the IDs of the allergenes that can be found in the food seperated by a comma.',
  `foodStatus` int(1) NOT NULL COMMENT '0=Not Available\r\n1=Available'
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `orders`
--

CREATE TABLE `orders` (
  `orderID` int(11) NOT NULL,
  `orderNote` varchar(150) NOT NULL COMMENT 'Additional note for the order.',
  `orderTime` datetime NOT NULL COMMENT 'The time when the order was taken.',
  `orderDueTime` datetime NOT NULL COMMENT 'The time when the order is due.',
  `orderDestID` int(4) NOT NULL,
  `orderDispatchID` int(3) NOT NULL COMMENT 'The ID of the delivery man if the orderType is 0 then its the ELVITEL users ID.',
  `orderStatus` int(1) NOT NULL COMMENT '0=In process\r\n1=Finished',
  `foodID` varchar(255) NOT NULL,
  `drinkID` varchar(255) NOT NULL,
  `paymentID` int(4) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `packaging`
--

CREATE TABLE `packaging` (
  `ID` int(11) NOT NULL,
  `price` double NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

--
-- A tábla adatainak kiíratása `packaging`
--

INSERT INTO `packaging` (`ID`, `price`) VALUES
(1, 200);

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `payment`
--

CREATE TABLE `payment` (
  `ID` int(11) NOT NULL,
  `paymentType` tinyint(1) NOT NULL COMMENT '0=Cash\r\n1=Card',
  `packagingCost` int(5) NOT NULL COMMENT 'Sum of the cost of the packages.',
  `deliveryCost` int(5) NOT NULL COMMENT 'If ther order type is 0, then this field is not used, otherwise its a given amount of money(distance based billing can be implemented in the future).',
  `sum` int(11) NOT NULL COMMENT 'Sum of the foods and drinks cost.(Packaging and delivery fees excluded)'
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `users`
--

CREATE TABLE `users` (
  `ID` int(11) NOT NULL,
  `username` varchar(20) NOT NULL COMMENT 'Log in username',
  `password` varchar(40) NOT NULL COMMENT 'Log in password',
  `dname` varchar(30) NOT NULL COMMENT 'Displayed name',
  `role` varchar(15) NOT NULL COMMENT 'Type of the user'
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

--
-- A tábla adatainak kiíratása `users`
--

INSERT INTO `users` (`ID`, `username`, `password`, `dname`, `role`) VALUES
(1, 'admin', 'a', 'Admin', 'admin');

--
-- Indexek a kiírt táblákhoz
--

--
-- A tábla indexei `allergene`
--
ALTER TABLE `allergene`
  ADD PRIMARY KEY (`allergeneID`);

--
-- A tábla indexei `customer`
--
ALTER TABLE `customer`
  ADD PRIMARY KEY (`ID`);

--
-- A tábla indexei `deliveryfee`
--
ALTER TABLE `deliveryfee`
  ADD PRIMARY KEY (`ID`);

--
-- A tábla indexei `destination`
--
ALTER TABLE `destination`
  ADD PRIMARY KEY (`ID`);

--
-- A tábla indexei `drink`
--
ALTER TABLE `drink`
  ADD PRIMARY KEY (`ID`);

--
-- A tábla indexei `finishedorders`
--
ALTER TABLE `finishedorders`
  ADD PRIMARY KEY (`forderID`);

--
-- A tábla indexei `food`
--
ALTER TABLE `food`
  ADD PRIMARY KEY (`ID`);

--
-- A tábla indexei `orders`
--
ALTER TABLE `orders`
  ADD PRIMARY KEY (`orderID`);

--
-- A tábla indexei `packaging`
--
ALTER TABLE `packaging`
  ADD PRIMARY KEY (`ID`);

--
-- A tábla indexei `payment`
--
ALTER TABLE `payment`
  ADD PRIMARY KEY (`ID`);

--
-- A tábla indexei `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`ID`);

--
-- A kiírt táblák AUTO_INCREMENT értéke
--

--
-- AUTO_INCREMENT a táblához `allergene`
--
ALTER TABLE `allergene`
  MODIFY `allergeneID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT a táblához `customer`
--
ALTER TABLE `customer`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT a táblához `deliveryfee`
--
ALTER TABLE `deliveryfee`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT a táblához `destination`
--
ALTER TABLE `destination`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT a táblához `drink`
--
ALTER TABLE `drink`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT a táblához `finishedorders`
--
ALTER TABLE `finishedorders`
  MODIFY `forderID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT a táblához `food`
--
ALTER TABLE `food`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT a táblához `orders`
--
ALTER TABLE `orders`
  MODIFY `orderID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT a táblához `packaging`
--
ALTER TABLE `packaging`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT a táblához `payment`
--
ALTER TABLE `payment`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT a táblához `users`
--
ALTER TABLE `users`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
