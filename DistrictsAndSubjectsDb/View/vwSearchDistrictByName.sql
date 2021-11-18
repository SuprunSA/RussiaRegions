create view [dbo].[vwSearchDistrictByName]
	as select * from [dbo].[District]
	where [Name] = N'Южный'
