SELECT RIGHT(Number, 1) AS LastNumber, Count(*) AS Count
FROM [BitLottery].[dbo].[Ballots]
Group by RIGHT(Number, 1)