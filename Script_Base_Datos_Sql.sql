USE [PruebaTecnica]
GO
/****** Object:  Table [dbo].[Autores]    Script Date: 16/01/2022 10:28:33 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Autores](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NombreCompleto] [varchar](100) NOT NULL,
	[FechaNacimiento] [date] NOT NULL,
	[CiudadDeProcedencia] [varchar](100) NOT NULL,
	[CorreoElectronico] [varchar](250) NOT NULL,
	[Estado] [bit] NOT NULL,
 CONSTRAINT [PK_Autores] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Configuraciones]    Script Date: 16/01/2022 10:28:33 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Configuraciones](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NumeroLibrosPermitido] [int] NOT NULL,
 CONSTRAINT [PK_Configuraciones] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Generos]    Script Date: 16/01/2022 10:28:33 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Generos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](20) NOT NULL,
 CONSTRAINT [PK_Genero] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Libros]    Script Date: 16/01/2022 10:28:33 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Libros](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Titulo] [varchar](200) NOT NULL,
	[Ano] [int] NOT NULL,
	[IdGenero] [int] NOT NULL,
	[NumeroDePaginas] [int] NOT NULL,
	[IdAutor] [int] NOT NULL,
	[Estado] [bit] NOT NULL,
 CONSTRAINT [PK_Libros] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Autores] ON 
GO
INSERT [dbo].[Autores] ([Id], [NombreCompleto], [FechaNacimiento], [CiudadDeProcedencia], [CorreoElectronico], [Estado]) VALUES (1, N'Juan prueba', CAST(N'1960-01-01' AS Date), N'bogota', N'juanprueba@gmail.com', 1)
GO
INSERT [dbo].[Autores] ([Id], [NombreCompleto], [FechaNacimiento], [CiudadDeProcedencia], [CorreoElectronico], [Estado]) VALUES (2, N'Juana de arco', CAST(N'2022-01-06' AS Date), N'francia -paris', N'juana@prueba.com', 1)
GO
SET IDENTITY_INSERT [dbo].[Autores] OFF
GO
SET IDENTITY_INSERT [dbo].[Configuraciones] ON 
GO
INSERT [dbo].[Configuraciones] ([Id], [NumeroLibrosPermitido]) VALUES (1, 3)
GO
SET IDENTITY_INSERT [dbo].[Configuraciones] OFF
GO
SET IDENTITY_INSERT [dbo].[Generos] ON 
GO
INSERT [dbo].[Generos] ([Id], [Nombre]) VALUES (1, N'Acción')
GO
INSERT [dbo].[Generos] ([Id], [Nombre]) VALUES (2, N'Comedia')
GO
INSERT [dbo].[Generos] ([Id], [Nombre]) VALUES (3, N'Drama')
GO
INSERT [dbo].[Generos] ([Id], [Nombre]) VALUES (4, N'Suspenso')
GO
SET IDENTITY_INSERT [dbo].[Generos] OFF
GO
SET IDENTITY_INSERT [dbo].[Libros] ON 
GO
INSERT [dbo].[Libros] ([Id], [Titulo], [Ano], [IdGenero], [NumeroDePaginas], [IdAutor], [Estado]) VALUES (1, N'Lo que se llevo2', 1990, 2, 23, 1, 1)
GO
INSERT [dbo].[Libros] ([Id], [Titulo], [Ano], [IdGenero], [NumeroDePaginas], [IdAutor], [Estado]) VALUES (3, N'La llorona', 2001, 3, 23, 2, 1)
GO
INSERT [dbo].[Libros] ([Id], [Titulo], [Ano], [IdGenero], [NumeroDePaginas], [IdAutor], [Estado]) VALUES (4, N'Dias lluvioso', 1980, 1, 32, 1, 1)
GO
SET IDENTITY_INSERT [dbo].[Libros] OFF
GO
ALTER TABLE [dbo].[Autores] ADD  CONSTRAINT [DF_Autores_Estado]  DEFAULT ((1)) FOR [Estado]
GO
ALTER TABLE [dbo].[Libros] ADD  CONSTRAINT [DF_Libros_Estado]  DEFAULT ((1)) FOR [Estado]
GO
ALTER TABLE [dbo].[Libros]  WITH CHECK ADD  CONSTRAINT [FK_Libros_Autores] FOREIGN KEY([IdAutor])
REFERENCES [dbo].[Autores] ([Id])
GO
ALTER TABLE [dbo].[Libros] CHECK CONSTRAINT [FK_Libros_Autores]
GO
ALTER TABLE [dbo].[Libros]  WITH CHECK ADD  CONSTRAINT [FK_Libros_Genero] FOREIGN KEY([IdGenero])
REFERENCES [dbo].[Generos] ([Id])
GO
ALTER TABLE [dbo].[Libros] CHECK CONSTRAINT [FK_Libros_Genero]
GO
