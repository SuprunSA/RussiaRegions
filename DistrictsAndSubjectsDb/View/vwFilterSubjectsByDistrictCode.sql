create view [dbo].[vwFilterSubjectsByDistrictCode]
	as select * from [dbo].[Subject]
	where District = 235