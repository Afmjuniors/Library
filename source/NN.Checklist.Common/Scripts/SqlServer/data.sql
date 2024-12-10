
INSERT INTO countries (name, prefix_number) values ('Brasil', 55);
INSERT INTO countries (name, prefix_number) values ('Estados unidos', 60);

INSERT INTO SYSTEMS_FUNCTIONALITIES (DESCRIPTION) VALUES ('SYSTEMS_ACCESSES');
INSERT INTO SYSTEMS_FUNCTIONALITIES (DESCRIPTION) VALUES ('PARAMETERS');
INSERT INTO SYSTEMS_FUNCTIONALITIES (DESCRIPTION) VALUES ('USERS');
INSERT INTO SYSTEMS_FUNCTIONALITIES (DESCRIPTION) VALUES ('PERMISSIONS');
INSERT INTO SYSTEMS_FUNCTIONALITIES (DESCRIPTION) VALUES ('GENERAL_REGISTRATIONS');


INSERT INTO PERMISSIONS (PERMISSION_ID, DESCRIPTION, NAME) VALUES (1, 'Manage parameters', 'MANAGE_PARAMETER');
INSERT INTO PERMISSIONS (PERMISSION_ID, DESCRIPTION, NAME) VALUES (2, 'Manage groups', 'MANAGE_GROUPS');
INSERT INTO PERMISSIONS (PERMISSION_ID, DESCRIPTION, NAME) VALUES (3, 'Manage countries areas and area phones', 'MANAGE_LOCALIZATION');
INSERT INTO PERMISSIONS (PERMISSION_ID, DESCRIPTION, NAME) VALUES (4, 'Manage users', 'MANAGE_USERS');
INSERT INTO PERMISSIONS (PERMISSION_ID, DESCRIPTION, NAME) VALUES (5, 'Manage Areas', 'MANAGE_AREAS');
INSERT INTO PERMISSIONS (PERMISSION_ID, DESCRIPTION, NAME) VALUES (6, 'Manage Checklists', 'CHECKLISTS');
INSERT INTO PERMISSIONS (PERMISSION_ID, DESCRIPTION, NAME) VALUES (7, 'Manage Validations', 'VALIDATIONS');

INSERT INTO AD_GROUPS (NAME, administrator) VALUES ('nsrm_admin', 1);
INSERT INTO AD_GROUPS (NAME, administrator) VALUES ('nsrm_manutencao', 0);
INSERT INTO AD_GROUPS (NAME, administrator) VALUES ('nsrm_avaliador', 0);
INSERT INTO AD_GROUPS (NAME, administrator) VALUES ('nsrm_qa', 0);

INSERT INTO AD_GROUPS_PERMISSIONS (AD_GROUP_ID, PERMISSION_ID) VALUEs (2, 1);
INSERT INTO AD_GROUPS_PERMISSIONS (AD_GROUP_ID, PERMISSION_ID) VALUEs (2, 1);
INSERT INTO AD_GROUPS_PERMISSIONS (AD_GROUP_ID, PERMISSION_ID) VALUEs (2, 2);
INSERT INTO AD_GROUPS_PERMISSIONS (AD_GROUP_ID, PERMISSION_ID) VALUEs (2, 3);

INSERT INTO  LANGUAGES (NAME, CODE, COUNTRY_ID) VALUES ('Português', 'pt-BR', 1);
INSERT INTO  LANGUAGES (NAME, CODE, COUNTRY_ID) VALUES ('Inglês', 'en-US', 2);

insert into users (initials, deactivated, datetime_deactivate, language_id) values ('_adm', 0, null, 1);

INSERT INTO PROCESSES (PROCESS_ID, DESCRIPTION, ACRONYM, CONNECTION_STRING_ALARMS, QUERY_ALARMS, CONNECTION_STRING_EVENTS, QUERY_EVENTS, FIRST_ID) VALUES (1, 'Clean', 'CU', 
' ', 
'select a.alarm_id, Start_Time as date_time_start, end_time as date_time_end, Ack_Time as date_time_ack, u.username as ack_user, aav.Value as system_node_name, a.Historian_Quality_Id,
a.Severity, Event_Reason_Id as type_event_category_id, Alarm_Desc as message, replace(s.Source, aav.Value + ''.'', '''') as tag, SUBSTRING(replace(s.Source, aav.Value + ''.'', ''''), 0, charindex(''_'', replace(s.Source, aav.Value + ''.'', ''''))) as area,
null as type_occurrence_level_id
from Alarms a
join Alarm_Types t on a.Alarm_Type_Id = t.Alarm_Type_Id
join source s on a.Source_Id = s.Source_Id
left join users u on a.Ack_By = u.User_Id
left join AlarmAttributeValues aav on a.Alarm_Id = aav.Alarm_Id and aav.Attribute_Id = 5
left join Event_Reasons er on a.EventCategory_Id = er.Event_Reason_Id
where a.alarm_id > @alarmId 
and a.Alarm_Type_Id = 1 and a.Source_Id is not null',
' ',
'select * from
(
select timestamp as date_time_start, a.tag as tag, a.area as area, a.description as comment, a.oldvalue as before_value, a.newvalue as new_value,
a.responsible as responsible, a.node as system_node_id, 4 as type_event_category_id
from ScadaAuditParameterChanged a 
where a.date_time_start > @pLastDateTime
union
select timestamp as date_time_start, a.tag as tag, a.area as area, a.description as comment, null as before_value, null as new_value,
a.performedby as responsible, a.node as system_node_id, 5 as type_event_category_id
from ScadaAuditParameterChanged a 
where a.date_time_start > @pLastDateTime
) as t
order by timestamp desc',
1);
INSERT INTO PROCESSES (PROCESS_ID, DESCRIPTION, ACRONYM, CONNECTION_STRING_ALARMS, QUERY_ALARMS, CONNECTION_STRING_EVENTS, QUERY_EVENTS, FIRST_ID) VALUES (2, 'Black', 'BU', 
' ', 
'select a.alarm_id, Start_Time as date_time_start, end_time as date_time_end, Ack_Time as date_time_ack, u.username as ack_user, aav.Value as system_node_name, a.Historian_Quality_Id,
a.Severity, Event_Reason_Id as type_event_category_id, Alarm_Desc as message, replace(s.Source, aav.Value + ''.'', '''') as tag, SUBSTRING(replace(s.Source, aav.Value + ''.'', ''''), 0, charindex(''_'', replace(s.Source, aav.Value + ''.'', ''''))) as area,
null as type_occurrence_level_id
from Alarms a
join Alarm_Types t on a.Alarm_Type_Id = t.Alarm_Type_Id
join source s on a.Source_Id = s.Source_Id
left join users u on a.Ack_By = u.User_Id
left join AlarmAttributeValues aav on a.Alarm_Id = aav.Alarm_Id and aav.Attribute_Id = 5
left join Event_Reasons er on a.EventCategory_Id = er.Event_Reason_Id
where and a.alarm_id > @alarmId 
and a.Alarm_Type_Id = 1 and a.Source_Id is not null',
' ', 
'select * from
(
select timestamp as date_time_start, a.tag as tag, a.area as area, a.description as comment, a.oldvalue as before_value, a.newvalue as new_value,
a.responsible as responsible, a.node as system_node_id, 4 as type_event_category_id
from ScadaAuditParameterChanged a 
where a.date_time_start > @pLastDateTime
union
select timestamp as date_time_start, a.tag as tag, a.area as area, a.description as comment, null as before_value, null as new_value,
a.performedby as responsible, a.node as system_node_id, 5 as type_event_category_id
from ScadaAuditParameterChanged a 
where a.date_time_start > @pLastDateTime
) as t
order by timestamp desc', 
1);
INSERT INTO PROCESSES (PROCESS_ID, DESCRIPTION, ACRONYM, CONNECTION_STRING_ALARMS, QUERY_ALARMS, CONNECTION_STRING_EVENTS, QUERY_EVENTS) VALUES (3, 'FMS', 'FMS', ' ', ' ', ' ', ' ');
INSERT INTO PROCESSES (PROCESS_ID, DESCRIPTION, ACRONYM, CONNECTION_STRING_ALARMS, QUERY_ALARMS, CONNECTION_STRING_EVENTS, QUERY_EVENTS) VALUES (4, 'BMS', 'BMS', ' ', ' ', ' ', ' ');

INSERT INTO AREAS (NAME, DESCRIPTION, PROCESS_ID) VALUES ('Automacao', 'Eventos relacionados ao funcionamento do SCADA Clean', '1');
INSERT INTO AREAS (NAME, DESCRIPTION, PROCESS_ID) VALUES ('9120', 'PW do QC', '1');
INSERT INTO AREAS (NAME, DESCRIPTION, PROCESS_ID) VALUES ('110A', 'PTW', '1');
INSERT INTO AREAS (NAME, DESCRIPTION, PROCESS_ID) VALUES ('110A1', 'PTW', '1');
INSERT INTO AREAS (NAME, DESCRIPTION, PROCESS_ID) VALUES ('120A', 'Armazenagem de PW', '1');
INSERT INTO AREAS (NAME, DESCRIPTION, PROCESS_ID) VALUES ('130A', 'Destilador A', '1');
INSERT INTO AREAS (NAME, DESCRIPTION, PROCESS_ID) VALUES ('130B', 'Destilador B', '1');
INSERT INTO AREAS (NAME, DESCRIPTION, PROCESS_ID) VALUES ('140A', 'Armazenagem de WFI', '1');
INSERT INTO AREAS (NAME, DESCRIPTION, PROCESS_ID) VALUES ('140B', 'Armazenagem de WFI e Distribuição para Linha A', '1');
INSERT INTO AREAS (NAME, DESCRIPTION, PROCESS_ID) VALUES ('140C', 'Armazenagem de WFI e Distribuição para Linha B', '1');
INSERT INTO AREAS (NAME, DESCRIPTION, PROCESS_ID) VALUES ('140D', 'Distribuição de WFI para pia do Wash Linha A', '1');
INSERT INTO AREAS (NAME, DESCRIPTION, PROCESS_ID) VALUES ('140E', 'Distribuição de WFI para Formulação Linha A', '1');
INSERT INTO AREAS (NAME, DESCRIPTION, PROCESS_ID) VALUES ('140F', 'Distribuição de WFI para Filling Linha A', '1');
INSERT INTO AREAS (NAME, DESCRIPTION, PROCESS_ID) VALUES ('140G', 'Distribuição de WFI para pia do Wash Linha B', '1');
INSERT INTO AREAS (NAME, DESCRIPTION, PROCESS_ID) VALUES ('140H', 'Distribuição de WFI para Formulação Linha B', '1');
INSERT INTO AREAS (NAME, DESCRIPTION, PROCESS_ID) VALUES ('140J', 'Distribuição de WFI para Filling Linha B', '1');
INSERT INTO AREAS (NAME, DESCRIPTION, PROCESS_ID) VALUES ('141A', 'Distribuição de Ar de Processo', '1');
INSERT INTO AREAS (NAME, DESCRIPTION, PROCESS_ID) VALUES ('141B', 'Distribuição de Ar de Processo', '1');
INSERT INTO AREAS (NAME, DESCRIPTION, PROCESS_ID) VALUES ('141C', 'Distribuição de Ar de Processo', '1');
INSERT INTO AREAS (NAME, DESCRIPTION, PROCESS_ID) VALUES ('141D', 'Distribuição de Ar de Processo', '1');
INSERT INTO AREAS (NAME, DESCRIPTION, PROCESS_ID) VALUES ('150A', 'Geração de Vapor Limpo', '1');
INSERT INTO AREAS (NAME, DESCRIPTION, PROCESS_ID) VALUES ('160A', 'Distribuição de Vapor Limpo', '1');
INSERT INTO AREAS (NAME, DESCRIPTION, PROCESS_ID) VALUES ('160B', 'Distribuição de Vapor Limpo', '1');
INSERT INTO AREAS (NAME, DESCRIPTION, PROCESS_ID) VALUES ('160C', 'Distribuição de Vapor Limpo', '1');
INSERT INTO AREAS (NAME, DESCRIPTION, PROCESS_ID) VALUES ('841A', 'Ar comprimido', '1');
INSERT INTO AREAS (NAME, DESCRIPTION, PROCESS_ID) VALUES ('Automacao', 'Eventos relacionados ao funcionamento do FMS', '3');
INSERT INTO AREAS (NAME, DESCRIPTION, PROCESS_ID) VALUES ('QC Micro', 'N/A', '3');
INSERT INTO AREAS (NAME, DESCRIPTION, PROCESS_ID) VALUES ('QC Químico', 'N/A', '3');
INSERT INTO AREAS (NAME, DESCRIPTION, PROCESS_ID) VALUES ('AP Processo', 'N/A', '3');
INSERT INTO AREAS (NAME, DESCRIPTION, PROCESS_ID) VALUES ('Inspection', 'N/A', '3');
INSERT INTO AREAS (NAME, DESCRIPTION, PROCESS_ID) VALUES ('FP', 'N/A', '3');
INSERT INTO AREAS (NAME, DESCRIPTION, PROCESS_ID) VALUES ('Almox', 'N/A', '3');
INSERT INTO AREAS (NAME, DESCRIPTION, PROCESS_ID) VALUES ('EP', 'N/A', '3');
INSERT INTO AREAS (NAME, DESCRIPTION, PROCESS_ID) VALUES ('IT', 'N/A', '3');
INSERT INTO AREAS (NAME, DESCRIPTION, PROCESS_ID) VALUES ('QC IM', 'N/A', '3');
INSERT INTO AREAS (NAME, DESCRIPTION, PROCESS_ID) VALUES ('QC EP', 'N/A', '3');
INSERT INTO AREAS (NAME, DESCRIPTION, PROCESS_ID) VALUES ('EM', 'N/A', '3');
INSERT INTO AREAS (NAME, DESCRIPTION, PROCESS_ID) VALUES ('HVAC', 'N/A', '3');
INSERT INTO AREAS (NAME, DESCRIPTION, PROCESS_ID) VALUES ('Automacao', 'Eventos relacionados ao funcionamento do SCADA Black', '2');
INSERT INTO AREAS (NAME, DESCRIPTION, PROCESS_ID) VALUES ('130A', 'Ar comprimido ALP', '2');
INSERT INTO AREAS (NAME, DESCRIPTION, PROCESS_ID) VALUES ('821A', 'Água de 6°C', '2');
INSERT INTO AREAS (NAME, DESCRIPTION, PROCESS_ID) VALUES ('811A', 'Torres de Resfriamento', '2');
INSERT INTO AREAS (NAME, DESCRIPTION, PROCESS_ID) VALUES ('831A', 'Água de 2°C', '2');
INSERT INTO AREAS (NAME, DESCRIPTION, PROCESS_ID) VALUES ('836A', 'Água de -3°C', '2');
INSERT INTO AREAS (NAME, DESCRIPTION, PROCESS_ID) VALUES ('841A', 'Ar Comprimido MOC2', '2');
INSERT INTO AREAS (NAME, DESCRIPTION, PROCESS_ID) VALUES ('851A', 'Reuso', '2');
INSERT INTO AREAS (NAME, DESCRIPTION, PROCESS_ID) VALUES ('871A', 'Água Quente', '2');
INSERT INTO AREAS (NAME, DESCRIPTION, PROCESS_ID) VALUES ('881A', 'Sistema de combate de incêndio', '2');
INSERT INTO AREAS (NAME, DESCRIPTION, PROCESS_ID) VALUES ('711A', 'Entrada da COPASA', '2');
INSERT INTO AREAS (NAME, DESCRIPTION, PROCESS_ID) VALUES ('731A', 'Armazenamento de combustível', '2');
INSERT INTO AREAS (NAME, DESCRIPTION, PROCESS_ID) VALUES ('757A', 'Subestação 600', '2');
INSERT INTO AREAS (NAME, DESCRIPTION, PROCESS_ID) VALUES ('757B', 'Subestação MOC1', '2');
INSERT INTO AREAS (NAME, DESCRIPTION, PROCESS_ID) VALUES ('801A/B/C/D', 'Caldeiras', '2');
INSERT INTO AREAS (NAME, DESCRIPTION, PROCESS_ID) VALUES ('802A', 'Desareador', '2');
