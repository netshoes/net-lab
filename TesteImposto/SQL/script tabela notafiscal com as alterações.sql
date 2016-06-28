USE [Teste]
GO

/****** Object:  Table [dbo].[NotaFiscal]    Script Date: 28/06/2016 15:28:25 ******/
DROP TABLE [dbo].[NotaFiscal]
GO

/****** Object:  Table [dbo].[NotaFiscal]    Script Date: 28/06/2016 15:28:25 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[NotaFiscal](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NumeroNotaFiscal] [int] NULL,
	[Serie] [int] NULL,
	[NomeCliente] [varchar](50) NULL,
	[EstadoDestino] [varchar](50) NULL,
	[EstadoOrigem] [varchar](50) NULL,
 CONSTRAINT [PK_NotaFiscal] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

