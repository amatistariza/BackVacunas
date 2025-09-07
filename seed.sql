/* Seed completo ajustado: Solo roles ADMINISTRADOR y ENFERMERO */
SET NOCOUNT ON;
PRINT '== INICIO SEED ==';

/* Limpieza (desactiva FK, borra, reactiva) */
PRINT 'Deshabilitando FKs';
EXEC sp_msforeachtable 'ALTER TABLE ? NOCHECK CONSTRAINT ALL';

PRINT 'Borrando datos';
DELETE FROM AlarmasVacunacion;
DELETE FROM RegistrosVacunacion;
DELETE FROM EsquemaVacunacionDetalles;
DELETE FROM EsquemasVacunacion;
DELETE FROM Antecedentes;
DELETE FROM CondicionesUsuarias;
DELETE FROM AntecedentesMedicos;
DELETE FROM Pacientes;
DELETE FROM Madres;
DELETE FROM Cuidadores;
DELETE FROM Vacunas;
DELETE FROM Jeringas;
DELETE FROM Diluyentes;
DELETE FROM Usuarios;

PRINT 'Reactivando FKs';
EXEC sp_msforeachtable 'ALTER TABLE ? WITH CHECK CHECK CONSTRAINT ALL';

/* Usuarios */
PRINT 'Insertando Usuarios';
INSERT INTO Usuarios (NombreUsuario,RolUser,Password) VALUES
('admin','ADMINISTRADOR','admin123'),
('enfermero1','ENFERMERO','enf123');

/* Catálogos e inventarios */
PRINT 'Insertando Vacunas';
INSERT INTO Vacunas (Nombre,Laboratorio,Lote,DosisDisponibles,FechaRegistro,NumeroDosis,IntervaloSemanas) VALUES
('BCG','LabBCG','BCG-001',200,GETUTCDATE(),1,0),
('Pentavalente','LabPenta','PENTA-010',300,GETUTCDATE(),3,8),
('Neumococo','LabNeo','NEU-050',250,GETUTCDATE(),3,8),
('Rotavirus','LabRota','ROTA-021',180,GETUTCDATE(),2,8);

PRINT 'Insertando Jeringas';
INSERT INTO Jeringas (Tipo,Lote,CantidadDisponible) VALUES
('23G1','J-2301',500),
('22G1','J-2201',400);

PRINT 'Insertando Diluyentes';
INSERT INTO Diluyentes (Nombre,Lote,CantidadDisponible) VALUES
('Cloruro Sodio 0.9%','DIL-001',300),
('Agua Bacteriostatica','DIL-002',200);


PRINT 'Insertando Madre y Cuidador';
INSERT INTO Madres (TipoIdentificacion,NumeroIdentificacion,PrimerNombre,PrimerApellido,CorreoElectronico,RegimenAfiliacion,PertenenciaEtnica,Desplazado)
VALUES ('CC','9001001','Maria','Lopez','maria@example.com','Contributivo','Mestizo',0);
INSERT INTO Cuidadores (TipoIdentificacion,NumeroIdentificacion,PrimerNombre,PrimerApellido,Parentesco,CorreoElectronico)
VALUES ('CC','8002001','Carlos','Gomez','Padre','carlos@example.com');

DECLARE @MadreId INT = (SELECT TOP 1 Id FROM Madres);
DECLARE @CuidadorId INT = (SELECT TOP 1 Id FROM Cuidadores);

PRINT 'Insertando Pacientes';
INSERT INTO Pacientes (FechaAtencion,TipoIdentificacion,NumeroIdentificacion,PrimerNombre,PrimerApellido,FechaNacimiento,EsquemaCompleto,Sexo,PaisNacimiento,EstatusMigratorio,RegimenAfiliacion,Aseguradora,PertenenciaEtnica,Desplazado,Discapacitado,Fallecido,VictimaConflictoArmado,EstudiaActualmente,PaisResidencia,DepartamentoResidencia,MunicipioResidencia,Area,AutorizaLlamadasTelefonicas,AutorizaEnvioCorreo,MadreId,CuidadorId)
VALUES
(GETUTCDATE(),'RC','1001','Ana','Perez','2024-01-15',0,'F','Colombia','Regular','Contributivo','Sura','Mestizo',0,0,0,0,0,'Colombia','Antioquia','Medellin','Urbana',1,1,@MadreId,@CuidadorId),
(GETUTCDATE(),'RC','1002','Luis','Gomez','2023-11-20',0,'M','Colombia','Regular','Subsidiado','NuevaEPS','Mestizo',0,0,0,0,0,'Colombia','Antioquia','Medellin','Urbana',1,1,NULL,NULL);

DECLARE @Paciente1 INT = (SELECT Id FROM Pacientes WHERE NumeroIdentificacion='1001');
DECLARE @Paciente2 INT = (SELECT Id FROM Pacientes WHERE NumeroIdentificacion='1002');

PRINT 'Insertando CondicionUsuaria y AntecedentesMedicos';
INSERT INTO CondicionesUsuarias (Condicion,Gestante,FechaUltimaMenstruacion,SemanasGestacion,FechaProbableParto,CantidadEmbarazosPrevios,PacienteId)
VALUES ('Sin condicion',0,NULL,NULL,NULL,0,@Paciente1);
INSERT INTO AntecedentesMedicos (ContraindicacionVacunacion,DetalleContraindicacion,ReaccionBiologicos,DetalleReaccion,PacienteId)
VALUES (0,NULL,0,NULL,@Paciente1);

PRINT 'Insertando Antecedentes';
INSERT INTO Antecedentes (FechaRegistro,Tipo,Descripcion,ObservacionesEspeciales,PacienteId) VALUES
(GETUTCDATE(),'Medico','Sin antecedentes relevantes',NULL,@Paciente1),
(GETUTCDATE(),'Medico','Sin antecedentes relevantes',NULL,@Paciente2);

PRINT 'Insertando Esquemas';
DECLARE @VacunaPenta INT = (SELECT Id FROM Vacunas WHERE Nombre='Pentavalente');
DECLARE @VacunaNeumo INT = (SELECT Id FROM Vacunas WHERE Nombre='Neumococo');
INSERT INTO EsquemasVacunacion (TipoCarnet,Responsable,RegistradoPAI,MotivoNoIngreso,Observaciones,PacienteId,VacunaId,CantidadTotalDosis,FrecuenciaAplicacion,DiasIntervalo,FechaPrimeraDosis)
VALUES ('INFANTIL','Enfermera Juana',1,NULL,'Esquema inicial',@Paciente1,@VacunaPenta,3,'semanal',NULL,GETUTCDATE());
DECLARE @Esquema1 INT = SCOPE_IDENTITY();
INSERT INTO EsquemasVacunacion (TipoCarnet,Responsable,RegistradoPAI,MotivoNoIngreso,Observaciones,PacienteId,VacunaId,CantidadTotalDosis,FrecuenciaAplicacion,DiasIntervalo,FechaPrimeraDosis)
VALUES ('INFANTIL','Enfermero Carlos',1,NULL,'Inicio pendiente',@Paciente2,@VacunaNeumo,3,'semanal',NULL,GETUTCDATE());
DECLARE @Esquema2 INT = SCOPE_IDENTITY();

PRINT 'Insertando Detalle primera dosis Pentavalente';
INSERT INTO EsquemaVacunacionDetalles (EsquemaVacunacionId,VacunaId,CantidadUtilizadaVacuna,FechaAplicacion,NumeroDosis,JeringaId)
VALUES (@Esquema1,@VacunaPenta,1,GETUTCDATE(),1,(SELECT TOP 1 Id FROM Jeringas));
DECLARE @Detalle1 INT = SCOPE_IDENTITY();

PRINT 'Insertando Alarma primera dosis';
INSERT INTO AlarmasVacunacion (PacienteId,VacunaId,DosisActual,FechaPrimeraAplicacion,FechaUltimaAplicacion,FechaProximaAplicacion,EsquemaCompletado,NotificacionEnviada,FechaNotificacion,Observaciones,EsquemaVacunacionDetalleId)
SELECT @Paciente1,@VacunaPenta,1,d.FechaAplicacion,d.FechaAplicacion,DATEADD(WEEK,v.IntervaloSemanas,d.FechaAplicacion),0,0,NULL,'Proxima dosis programada',@Detalle1
FROM EsquemaVacunacionDetalles d
JOIN Vacunas v ON v.Id=d.VacunaId
WHERE d.Id=@Detalle1;

PRINT 'Insertando RegistroVacunacion';
INSERT INTO RegistrosVacunacion (PacienteId,VacunaId,EsquemaVacunacionId,NumeroDosis,FechaAplicacion,FechaProximaDosis,FechaRegistro,EstadoRegistro,Observaciones,UsuarioRegistro)
SELECT @Paciente1,@VacunaPenta,@Esquema1,1,FechaAplicacion,DATEADD(WEEK,8,FechaAplicacion),GETUTCDATE(),'Aplicada','Primera dosis aplicada','admin' FROM EsquemaVacunacionDetalles WHERE Id=@Detalle1;

PRINT 'Resumen conteos';
SELECT 'Usuarios' Tabla, COUNT(*) Total FROM Usuarios UNION ALL
SELECT 'Vacunas', COUNT(*) FROM Vacunas UNION ALL
SELECT 'Pacientes', COUNT(*) FROM Pacientes UNION ALL
SELECT 'EsquemasVacunacion', COUNT(*) FROM EsquemasVacunacion UNION ALL
SELECT 'EsquemaVacunacionDetalles', COUNT(*) FROM EsquemaVacunacionDetalles UNION ALL
SELECT 'AlarmasVacunacion', COUNT(*) FROM AlarmasVacunacion UNION ALL
SELECT 'RegistrosVacunacion', COUNT(*) FROM RegistrosVacunacion;

PRINT '== FIN SEED ==';
