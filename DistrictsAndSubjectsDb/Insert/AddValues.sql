use [DistrictsAndSubjectsDb]
go

insert into [dbo].[District]
		    ([Code]
		    ,[Name])
	values 
		    (235, N'Центральный')
		    ,(7421, N'Южный')
go

insert into [dbo].[Subject]
		    ([code]
		    ,[Name]
		    ,[AdminCenterName]
		    ,[Population]
		    ,[Square]
		    ,[District])
	values
		    (1234512, N'Самарская область', N'Самара', 1200.124, 233424.34, 235),
			(2342351, N'Ульяновская область', N'Ульяновск', 645.124, 232345.41, 235),
			(764575, N'Московская область', N'Москва', 12435.432, 4562347.93, 235),
			(4353, N'Краснодарский край', N'Краснодар', 3456.654, 2743265.75, 7421)
go
