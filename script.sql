USE [Demo]
GO
/****** Object:  Table [dbo].[MVCregUser]    Script Date: 03/01/2023 21:35:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MVCregUser](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Uname] [varchar](100) NULL,
	[Uemail] [varchar](100) NULL,
	[Upwd] [varchar](50) NULL,
	[Umob] [varchar](50) NULL,
	[Ucountry] [varchar](50) NULL,
	[Uimg] [varchar](max) NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedDate]  AS (getdate()),
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
