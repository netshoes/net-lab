USE [Teste]
GO

ALTER TABLE [dbo].[NotaFiscalItem] DROP CONSTRAINT [FK_NotaFiscal]
GO

/****** Object:  Table [dbo].[NotaFiscalItem]    Script Date: 28/06/2016 15:28:42 ******/
DROP TABLE [dbo].[NotaFiscalItem]
GO

/****** Object:  Table [dbo].[NotaFiscalItem]    Script Date: 28/06/2016 15:28:42 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[NotaFiscalItem](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdNotaFiscal] [int] NULL,
	[Cfop] [varchar](5) NULL,
	[TipoIcms] [varchar](20) NULL,
	[BaseIcms] [decimal](18, 5) NULL,
	[AliquotaIcms] [decimal](18, 5) NULL,
	[ValorIcms] [decimal](18, 5) NULL,
	[NomeProduto] [varchar](50) NULL,
	[CodigoProduto] [varchar](20) NULL,
	[BaseIpi] [decimal](18, 5) NULL,
	[ValorIpi] [decimal](18, 5) NULL,
	[Desconto] [decimal](18, 5) NULL,
	[AliquotaIpi] [decimal](18, 5) NULL,
 CONSTRAINT [PK_NotaFiscalItem] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[NotaFiscalItem]  WITH CHECK ADD  CONSTRAINT [FK_NotaFiscal] FOREIGN KEY([Id])
REFERENCES [dbo].[NotaFiscalItem] ([Id])
GO

ALTER TABLE [dbo].[NotaFiscalItem] CHECK CONSTRAINT [FK_NotaFiscal]
GO

