
INSERT INTO countries (name, prefix_number) values ('Brasil', 55); 
INSERT INTO countries (name, prefix_number) values ('Estados unidos', 60); 
 
INSERT INTO SYSTEMS_FUNCTIONALITIES (DESCRIPTION) VALUES ('SYSTEMS_ACCESSES'); 
INSERT INTO SYSTEMS_FUNCTIONALITIES (DESCRIPTION) VALUES ('PARAMETERS'); 
INSERT INTO SYSTEMS_FUNCTIONALITIES (DESCRIPTION) VALUES ('USERS'); 
INSERT INTO SYSTEMS_FUNCTIONALITIES (DESCRIPTION) VALUES ('PERMISSIONS'); 
INSERT INTO SYSTEMS_FUNCTIONALITIES (DESCRIPTION) VALUES ('GENERAL_REGISTRATIONS'); 
INSERT INTO SYSTEMS_FUNCTIONALITIES (DESCRIPTION) VALUES ('CHECKLISTS'); 

 
 
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
 
INSERT INTO CHECKLISTS_TEMPLATES (description) VALUES ('Checklist A12 AP - ANP: Approval for next process'); 
INSERT INTO CHECKLISTS_TEMPLATES (description) VALUES ('Checklist A12 AP - FSA usando DBR'); 
INSERT INTO CHECKLISTS_TEMPLATES (description) VALUES ('Checklist A12 AP - FSA: Bulk De STJ, CH e CL recebidos em MOC'); 
INSERT INTO CHECKLISTS_TEMPLATES (description) VALUES ('Checklist A12 Bulk'); 
 
INSERT INTO VERSIONS_CHECKLISTS_TEMPLATES (checklist_template_id, timestamp_creation, timestamp_update, version, creation_user_id, update_user_id)  
select c.checklist_template_id, getdate(), null, '1.0.0', u.user_id, null  
from checklists_templates c  cross join users u where c.description = 'Checklist A12 AP - ANP: Approval for next process' and initials = '_adm'; 
 
INSERT INTO VERSIONS_CHECKLISTS_TEMPLATES (checklist_template_id, timestamp_creation, timestamp_update, version, creation_user_id, update_user_id)  
select c.checklist_template_id, getdate(), null, '1.0.0', u.user_id, null  
from checklists_templates c  cross join users u where c.description = 'Checklist A12 AP - FSA usando DBR' and initials = '_adm'; 
 
INSERT INTO VERSIONS_CHECKLISTS_TEMPLATES (checklist_template_id, timestamp_creation, timestamp_update, version, creation_user_id, update_user_id)  
select c.checklist_template_id, getdate(), null, '1.0.0', u.user_id, null  
from checklists_templates c  cross join users u where c.description = 'Checklist A12 AP - FSA: Bulk De STJ, CH e CL recebidos em MOC' and initials = '_adm'; 
            
INSERT INTO VERSIONS_CHECKLISTS_TEMPLATES (checklist_template_id, timestamp_creation, timestamp_update, version, creation_user_id, update_user_id)   
select c.checklist_template_id, getdate(), null, '1.0.0', u.user_id, null  
from checklists_templates c  cross join users u where c.description = 'Checklist A12 Bulk' and initials = '_adm'; 
 
INSERT INTO FIELDS_DATA_TYPES (field_data_type_id, name) VALUES (1,'Text'); 
INSERT INTO FIELDS_DATA_TYPES (field_data_type_id, name) VALUES (2,'Number'); 
INSERT INTO FIELDS_DATA_TYPES (field_data_type_id, name) VALUES (3,'Date'); 
INSERT INTO FIELDS_DATA_TYPES (field_data_type_id, name) VALUES (4,'Options'); 
 

INSERT INTO FIELDS_VERSIONS_CHECKLISTS_TEMPLATES (version_checklist_template_id, position, title, field_data_type_id, regex_validation, format, mandatory, is_key)  
select v.version_checklist_template_id, 1, 'Batch Number (Formulation)',1, '', null, 1, 1  
from VERSIONS_CHECKLISTS_TEMPLATES v  
join checklists_templates c on v.checklist_template_id = c.checklist_template_id  
join users u on v.creation_user_id = u.user_id and initials = '_adm'  
where c.description = 'Checklist A12 AP - ANP: Approval for next process'; 
 
INSERT INTO FIELDS_VERSIONS_CHECKLISTS_TEMPLATES (version_checklist_template_id, position, title, field_data_type_id, regex_validation, format, mandatory, is_key)  
select v.version_checklist_template_id, 3, 'Item number',2, null, null, 1, 0  
from VERSIONS_CHECKLISTS_TEMPLATES v  
join checklists_templates c on v.checklist_template_id = c.checklist_template_id  
cross join users u  
where c.description = 'Checklist A12 AP - ANP: Approval for next process' and initials = '_adm'; 
 
INSERT INTO FIELDS_VERSIONS_CHECKLISTS_TEMPLATES (version_checklist_template_id, position, title, field_data_type_id, regex_validation, format, mandatory, is_key)  
select v.version_checklist_template_id, 4, 'Batch Number (Uninspected)',1, '', null, 1, 0  
from VERSIONS_CHECKLISTS_TEMPLATES v  
join checklists_templates c on v.checklist_template_id = c.checklist_template_id  
cross join users u  
where c.description = 'Checklist A12 AP - ANP: Approval for next process' and initials = '_adm'; 
 
INSERT INTO FIELDS_VERSIONS_CHECKLISTS_TEMPLATES (version_checklist_template_id, position, title, field_data_type_id, regex_validation, format, mandatory, is_key)  
select v.version_checklist_template_id, 5, 'Item number',2, null, null, 1, 0  
from VERSIONS_CHECKLISTS_TEMPLATES v  
join checklists_templates c on v.checklist_template_id = c.checklist_template_id  
cross join users u  
where c.description = 'Checklist A12 AP - ANP: Approval for next process' and initials = '_adm'; 
 
INSERT INTO FIELDS_VERSIONS_CHECKLISTS_TEMPLATES (version_checklist_template_id, position, title, field_data_type_id, regex_validation, format, mandatory, is_key)  
select v.version_checklist_template_id, 6, 'Batch Number (Inspected)',1, '', null, 1, 0  
from VERSIONS_CHECKLISTS_TEMPLATES v  
join checklists_templates c on v.checklist_template_id = c.checklist_template_id  
cross join users u  
where c.description = 'Checklist A12 AP - ANP: Approval for next process' and initials = '_adm'; 
 
INSERT INTO FIELDS_VERSIONS_CHECKLISTS_TEMPLATES (version_checklist_template_id, position, title, field_data_type_id, regex_validation, format, mandatory, is_key)  
select v.version_checklist_template_id, 7, 'Item number',2, null, null, 1, 0  
from VERSIONS_CHECKLISTS_TEMPLATES v  
join checklists_templates c on v.checklist_template_id = c.checklist_template_id  
cross join users u  
where c.description = 'Checklist A12 AP - ANP: Approval for next process' and initials = '_adm'; 
 
INSERT INTO FIELDS_VERSIONS_CHECKLISTS_TEMPLATES (version_checklist_template_id, position, title, field_data_type_id, regex_validation, format, mandatory, is_key)  
select v.version_checklist_template_id, 8, 'Item name',1, null, null, 1, 0  
from VERSIONS_CHECKLISTS_TEMPLATES v  
join checklists_templates c on v.checklist_template_id = c.checklist_template_id  
cross join users u  
where c.description = 'Checklist A12 AP - ANP: Approval for next process' and initials = '_adm'; 
 
INSERT INTO FIELDS_VERSIONS_CHECKLISTS_TEMPLATES (version_checklist_template_id, position, title, field_data_type_id, regex_validation, format, mandatory, is_key)  
select v.version_checklist_template_id, 9, 'Production Type',4, null, null, 1, 0  
from VERSIONS_CHECKLISTS_TEMPLATES v  
join checklists_templates c on v.checklist_template_id = c.checklist_template_id  
cross join users u  
where c.description = 'Checklist A12 AP - ANP: Approval for next process' and initials = '_adm'; 
INSERT INTO FIELDS_VERSIONS_CHECKLISTS_TEMPLATES (version_checklist_template_id, position, title, field_data_type_id, regex_validation, format, mandatory, is_key)  
select v.version_checklist_template_id, 2, 'Date',3, null, null, 1, 0  
from VERSIONS_CHECKLISTS_TEMPLATES v  
join checklists_templates c on v.checklist_template_id = c.checklist_template_id  
cross join users u  
where c.description = 'Checklist A12 AP - ANP: Approval for next process' and initials = '_adm'; 
 
INSERT INTO OPTIONS_FIELDS_VERSIONS_CHECKLISTS_TEMPLATES (field_version_checklist_template_id,title,value)  
select f.field_version_checklist_template_id, 'Exportação Direta', 1  
from VERSIONS_CHECKLISTS_TEMPLATES v  
join checklists_templates c on v.checklist_template_id = c.checklist_template_id  
join FIELDS_VERSIONS_CHECKLISTS_TEMPLATES f on v.version_checklist_template_id = f.version_checklist_template_id  
where c.description = 'Checklist A12 AP - ANP: Approval for next process' and f.position = 9;  
 
INSERT INTO OPTIONS_FIELDS_VERSIONS_CHECKLISTS_TEMPLATES (field_version_checklist_template_id,title,value)  
select f.field_version_checklist_template_id, 'Produção Contratada', 2  
from VERSIONS_CHECKLISTS_TEMPLATES v  
join checklists_templates c on v.checklist_template_id = c.checklist_template_id  
join FIELDS_VERSIONS_CHECKLISTS_TEMPLATES f on v.version_checklist_template_id = f.version_checklist_template_id  
where c.description = 'Checklist A12 AP - ANP: Approval for next process' and f.position = 9; 
 
INSERT INTO BLOCKS_VERSIONS_CHECKLISTS_TEMPLATES (version_checklist_template_id, position, title)  
select v.version_checklist_template_id, 1, 'Parte 1: Aprovação para o Próximo Processo'  
from VERSIONS_CHECKLISTS_TEMPLATES v  
join checklists_templates c on v.checklist_template_id = c.checklist_template_id  
where c.description = 'Checklist A12 AP - ANP: Approval for next process'; 
 
INSERT INTO BLOCKS_VERSIONS_CHECKLISTS_TEMPLATES (version_checklist_template_id, position, title)  
select v.version_checklist_template_id, 2, 'Status Assignment, ANP'  
from VERSIONS_CHECKLISTS_TEMPLATES v  
join checklists_templates c on v.checklist_template_id = c.checklist_template_id  
where c.description = 'Checklist A12 AP - ANP: Approval for next process'; 
 
INSERT INTO BLOCKS_VERSIONS_CHECKLISTS_TEMPLATES (version_checklist_template_id, position, title, parent_block_version_checklist_template_id)  
select v.version_checklist_template_id, 2, 'Verificação', b.block_version_checklist_template_id  
from VERSIONS_CHECKLISTS_TEMPLATES v  
join checklists_templates c on v.checklist_template_id = c.checklist_template_id  
join BLOCKS_VERSIONS_CHECKLISTS_TEMPLATES b on b.version_checklist_template_id = v.version_checklist_template_id  
where c.description = 'Checklist A12 AP - ANP: Approval for next process' and b.position = 2  and b.parent_block_version_checklist_template_id is NULL; 
 
INSERT INTO BLOCKS_VERSIONS_CHECKLISTS_TEMPLATES (version_checklist_template_id, position, title)   
select v.version_checklist_template_id, 3, 'Parte 2: ANP / Final Status Assignment'  
from VERSIONS_CHECKLISTS_TEMPLATES v  
join checklists_templates c on v.checklist_template_id = c.checklist_template_id  
where c.description = 'Checklist A12 AP - ANP: Approval for next process'; 

INSERT INTO BLOCKS_VERSIONS_CHECKLISTS_TEMPLATES (version_checklist_template_id, position, title)  
select v.version_checklist_template_id, 1, 'Passos' 
from VERSIONS_CHECKLISTS_TEMPLATES v 
join checklists_templates c on v.checklist_template_id = c.checklist_template_id 
where c.description = 'Checklist A12 AP - FSA: Bulk De STJ, CH e CL recebidos em MOC'; 
 
INSERT INTO BLOCKS_VERSIONS_CHECKLISTS_TEMPLATES (version_checklist_template_id, position, title)  
select v.version_checklist_template_id, 2, 'Final Status assignment in SAP (considere as etapas anteriores para definição do status do lote)' 
from VERSIONS_CHECKLISTS_TEMPLATES v 
join checklists_templates c on v.checklist_template_id = c.checklist_template_id 
where c.description = 'Checklist A12 AP - FSA: Bulk De STJ, CH e CL recebidos em MOC'; 
 
INSERT INTO BLOCKS_VERSIONS_CHECKLISTS_TEMPLATES (version_checklist_template_id, position, title, parent_block_version_checklist_template_id)  
select v.version_checklist_template_id, 2, 'Verificação', b.block_version_checklist_template_id 
from VERSIONS_CHECKLISTS_TEMPLATES v 
join checklists_templates c on v.checklist_template_id = c.checklist_template_id 
join BLOCKS_VERSIONS_CHECKLISTS_TEMPLATES b on b.version_checklist_template_id = v.version_checklist_template_id 
where c.description = 'Checklist A12 AP - FSA: Bulk De STJ, CH e CL recebidos em MOC' and b.position = 2 and b.parent_block_version_checklist_template_id is null; 
 
 
INSERT INTO ITEMS_TYPES (item_type_id, description) VALUES (1, 'Check Item'); 
INSERT INTO ITEMS_TYPES (item_type_id, description) VALUES (2, 'Double Check Item'); 
INSERT INTO ITEMS_TYPES (item_type_id, description) VALUES (3, 'Check Item one option'); 
INSERT INTO ITEMS_TYPES (item_type_id, description) VALUES (4, 'Check Item many options'); 
 
INSERT INTO ITEMS_VERSIONS_CHECKLISTS_TEMPLATES (version_checklist_template_id,position,title,item_type_id,block_version_checklist_template_id,option_field_version_checklist_template_id)  
select v.version_checklist_template_id, 1, 'Versão do CheckList', 1, b.block_version_checklist_template_id, null  
from VERSIONS_CHECKLISTS_TEMPLATES v  
join checklists_templates c on v.checklist_template_id = c.checklist_template_id  
join BLOCKS_VERSIONS_CHECKLISTS_TEMPLATES b on v.version_checklist_template_id = b.version_checklist_template_id  
where c.description = 'Checklist A12 AP - ANP: Approval for next process' and b.position = 1; 
 
INSERT INTO ITEMS_VERSIONS_CHECKLISTS_TEMPLATES (version_checklist_template_id,position,title,item_type_id,block_version_checklist_template_id,option_field_version_checklist_template_id)  
select v.version_checklist_template_id, 2, 'Consistência entre sistema e checklist', 1, b.block_version_checklist_template_id, null  
from VERSIONS_CHECKLISTS_TEMPLATES v  
join checklists_templates c on v.checklist_template_id = c.checklist_template_id  
join BLOCKS_VERSIONS_CHECKLISTS_TEMPLATES b on v.version_checklist_template_id = b.version_checklist_template_id  
where c.description = 'Checklist A12 AP - ANP: Approval for next process' and b.position = 1; 
 
INSERT INTO ITEMS_VERSIONS_CHECKLISTS_TEMPLATES (version_checklist_template_id,position,title,item_type_id,block_version_checklist_template_id,option_field_version_checklist_template_id)  
select v.version_checklist_template_id, 3, 'BPRs Formulação, Enchimento e Inspeção', 1, b.block_version_checklist_template_id, null  
from VERSIONS_CHECKLISTS_TEMPLATES v  
join checklists_templates c on v.checklist_template_id = c.checklist_template_id  
join BLOCKS_VERSIONS_CHECKLISTS_TEMPLATES b on v.version_checklist_template_id = b.version_checklist_template_id  
where c.description = 'Checklist A12 AP - ANP: Approval for next process' and b.position = 1;  
 
INSERT INTO ITEMS_VERSIONS_CHECKLISTS_TEMPLATES (version_checklist_template_id,position,title,item_type_id,block_version_checklist_template_id,option_field_version_checklist_template_id)  
select v.version_checklist_template_id, 4, 'Wash & Sterilisation', 1, b.block_version_checklist_template_id, null  
from VERSIONS_CHECKLISTS_TEMPLATES v  
join checklists_templates c on v.checklist_template_id = c.checklist_template_id  
join BLOCKS_VERSIONS_CHECKLISTS_TEMPLATES b on v.version_checklist_template_id = b.version_checklist_template_id  
where c.description = 'Checklist A12 AP - ANP: Approval for next process' and b.position = 1; 
 
INSERT INTO ITEMS_VERSIONS_CHECKLISTS_TEMPLATES (version_checklist_template_id,position,title,item_type_id,block_version_checklist_template_id,option_field_version_checklist_template_id)  
select v.version_checklist_template_id, 5, 'Incoming Material', 1, b.block_version_checklist_template_id, null  
from VERSIONS_CHECKLISTS_TEMPLATES v  
join checklists_templates c on v.checklist_template_id = c.checklist_template_id  
join BLOCKS_VERSIONS_CHECKLISTS_TEMPLATES b on v.version_checklist_template_id = b.version_checklist_template_id  
where c.description = 'Checklist A12 AP - ANP: Approval for next process' and b.position = 1; 
 
INSERT INTO ITEMS_VERSIONS_CHECKLISTS_TEMPLATES (version_checklist_template_id,position,title,item_type_id,block_version_checklist_template_id,option_field_version_checklist_template_id)  
select v.version_checklist_template_id, 6, 'Facility Monitoring System (FMS)', 1, b.block_version_checklist_template_id, null  
from VERSIONS_CHECKLISTS_TEMPLATES v  
join checklists_templates c on v.checklist_template_id = c.checklist_template_id  
join BLOCKS_VERSIONS_CHECKLISTS_TEMPLATES b on v.version_checklist_template_id = b.version_checklist_template_id  
where c.description = 'Checklist A12 AP - ANP: Approval for next process' and b.position = 1;  
 
INSERT INTO ITEMS_VERSIONS_CHECKLISTS_TEMPLATES (version_checklist_template_id,position,title,item_type_id,block_version_checklist_template_id,option_field_version_checklist_template_id)  
select v.version_checklist_template_id, 7, 'Resultados dos testes de QC (incluindo teste de esterilidade, se disponível, no LIMS)', 1, b.block_version_checklist_template_id, null  
from VERSIONS_CHECKLISTS_TEMPLATES v  
join checklists_templates c on v.checklist_template_id = c.checklist_template_id  
join BLOCKS_VERSIONS_CHECKLISTS_TEMPLATES b on v.version_checklist_template_id = b.version_checklist_template_id  
where c.description = 'Checklist A12 AP - ANP: Approval for next process' and b.position = 1;  
 
INSERT INTO ITEMS_VERSIONS_CHECKLISTS_TEMPLATES (version_checklist_template_id,position,title,item_type_id,block_version_checklist_template_id,option_field_version_checklist_template_id)  
select v.version_checklist_template_id, 8, 'Desvios', 1, b.block_version_checklist_template_id, null  
from VERSIONS_CHECKLISTS_TEMPLATES v  
join checklists_templates c on v.checklist_template_id = c.checklist_template_id  
join BLOCKS_VERSIONS_CHECKLISTS_TEMPLATES b on v.version_checklist_template_id = b.version_checklist_template_id  
where c.description = 'Checklist A12 AP - ANP: Approval for next process' and b.position = 1; 
 
INSERT INTO ITEMS_VERSIONS_CHECKLISTS_TEMPLATES (version_checklist_template_id,position,title,item_type_id,block_version_checklist_template_id,option_field_version_checklist_template_id)  
select v.version_checklist_template_id, 9, 'Formulário que contém informações com impacto em liberação de lote', 1, b.block_version_checklist_template_id, null  
from VERSIONS_CHECKLISTS_TEMPLATES v  
join checklists_templates c on v.checklist_template_id = c.checklist_template_id  
join BLOCKS_VERSIONS_CHECKLISTS_TEMPLATES b on v.version_checklist_template_id = b.version_checklist_template_id  
where c.description = 'Checklist A12 AP - ANP: Approval for next process' and b.position = 1; 
 
INSERT INTO ITEMS_VERSIONS_CHECKLISTS_TEMPLATES (version_checklist_template_id,position,title,item_type_id,block_version_checklist_template_id,option_field_version_checklist_template_id)  
select v.version_checklist_template_id, 10, 'CRs do novoGloW', 1, b.block_version_checklist_template_id, null  
from VERSIONS_CHECKLISTS_TEMPLATES v  
join checklists_templates c on v.checklist_template_id = c.checklist_template_id  
join BLOCKS_VERSIONS_CHECKLISTS_TEMPLATES b on v.version_checklist_template_id = b.version_checklist_template_id  
where c.description = 'Checklist A12 AP - ANP: Approval for next process' and b.position = 1; 
 
INSERT INTO ITEMS_VERSIONS_CHECKLISTS_TEMPLATES (version_checklist_template_id,position,title,item_type_id,block_version_checklist_template_id,option_field_version_checklist_template_id)  
select v.version_checklist_template_id, 11, 'NNTZW', 1, b.block_version_checklist_template_id, null  
from VERSIONS_CHECKLISTS_TEMPLATES v  
join checklists_templates c on v.checklist_template_id = c.checklist_template_id  
join BLOCKS_VERSIONS_CHECKLISTS_TEMPLATES b on v.version_checklist_template_id = b.version_checklist_template_id  
where c.description = 'Checklist A12 AP - ANP: Approval for next process' and b.position = 1; 
 
INSERT INTO ITEMS_VERSIONS_CHECKLISTS_TEMPLATES (version_checklist_template_id,position,title,item_type_id,block_version_checklist_template_id,option_field_version_checklist_template_id)  
select v.version_checklist_template_id, 12, 'Date of manufacture no SAP', 1, b.block_version_checklist_template_id, null  
from VERSIONS_CHECKLISTS_TEMPLATES v  
join checklists_templates c on v.checklist_template_id = c.checklist_template_id  
join BLOCKS_VERSIONS_CHECKLISTS_TEMPLATES b on v.version_checklist_template_id = b.version_checklist_template_id  
where c.description = 'Checklist A12 AP - ANP: Approval for next process' and b.position = 1; 
 
INSERT INTO ITEMS_VERSIONS_CHECKLISTS_TEMPLATES (version_checklist_template_id,position,title,item_type_id,block_version_checklist_template_id,option_field_version_checklist_template_id)  
select v.version_checklist_template_id, 13, 'Quantity of Penfill', 1, b.block_version_checklist_template_id, null  
from VERSIONS_CHECKLISTS_TEMPLATES v  
join checklists_templates c on v.checklist_template_id = c.checklist_template_id  
join BLOCKS_VERSIONS_CHECKLISTS_TEMPLATES b on v.version_checklist_template_id = b.version_checklist_template_id  
where c.description = 'Checklist A12 AP - ANP: Approval for next process' and b.position = 1; 
 
INSERT INTO ITEMS_VERSIONS_CHECKLISTS_TEMPLATES (version_checklist_template_id,position,title,item_type_id,block_version_checklist_template_id,option_field_version_checklist_template_id)  
select v.version_checklist_template_id, 14, 'Limitações no SAP', 1, b.block_version_checklist_template_id, null  
from VERSIONS_CHECKLISTS_TEMPLATES v  
join checklists_templates c on v.checklist_template_id = c.checklist_template_id  
join BLOCKS_VERSIONS_CHECKLISTS_TEMPLATES b on v.version_checklist_template_id = b.version_checklist_template_id  
where c.description = 'Checklist A12 AP - ANP: Approval for next process' and b.position = 1; 
 
INSERT INTO ITEMS_VERSIONS_CHECKLISTS_TEMPLATES (version_checklist_template_id,position,title,item_type_id,block_version_checklist_template_id,option_field_version_checklist_template_id)  
select v.version_checklist_template_id, 1, 'Status assignment, ANP', 1, b.block_version_checklist_template_id, null  
from VERSIONS_CHECKLISTS_TEMPLATES v  
join checklists_templates c on v.checklist_template_id = c.checklist_template_id  
join BLOCKS_VERSIONS_CHECKLISTS_TEMPLATES b on v.version_checklist_template_id = b.version_checklist_template_id  
where c.description = 'Checklist A12 AP - ANP: Approval for next process' and b.position = 2  and b.parent_block_version_checklist_template_id is NULL; 
 
INSERT INTO ITEMS_VERSIONS_CHECKLISTS_TEMPLATES (version_checklist_template_id,position,title,item_type_id,block_version_checklist_template_id,option_field_version_checklist_template_id)  
select v.version_checklist_template_id, 1, 'Status assignment correto no SAP', 1, b.block_version_checklist_template_id, null  
from VERSIONS_CHECKLISTS_TEMPLATES v  
join checklists_templates c on v.checklist_template_id = c.checklist_template_id  
join BLOCKS_VERSIONS_CHECKLISTS_TEMPLATES b on v.version_checklist_template_id = b.version_checklist_template_id  
where c.description = 'Checklist A12 AP - ANP: Approval for next process' and b.position = 2  and b.parent_block_version_checklist_template_id is NOT NULL; 
 
INSERT INTO ITEMS_VERSIONS_CHECKLISTS_TEMPLATES (version_checklist_template_id,position,title,item_type_id,block_version_checklist_template_id,option_field_version_checklist_template_id)  
select v.version_checklist_template_id, 2, 'Limitações corretas no notebook do SAP (NA se feito em um único fluxo)', 1, b.block_version_checklist_template_id, null  
from VERSIONS_CHECKLISTS_TEMPLATES v  
join checklists_templates c on v.checklist_template_id = c.checklist_template_id  
join BLOCKS_VERSIONS_CHECKLISTS_TEMPLATES b on v.version_checklist_template_id = b.version_checklist_template_id  
where c.description = 'Checklist A12 AP - ANP: Approval for next process' and b.position = 2  and b.parent_block_version_checklist_template_id is NOT NULL;  
 
INSERT INTO ITEMS_VERSIONS_CHECKLISTS_TEMPLATES (version_checklist_template_id,position,title,item_type_id,block_version_checklist_template_id,option_field_version_checklist_template_id)  
select v.version_checklist_template_id, 3, 'Desvios Maiores', 1, b.block_version_checklist_template_id, null  
from VERSIONS_CHECKLISTS_TEMPLATES v  
join checklists_templates c on v.checklist_template_id = c.checklist_template_id  
join BLOCKS_VERSIONS_CHECKLISTS_TEMPLATES b on v.version_checklist_template_id = b.version_checklist_template_id  
where c.description = 'Checklist A12 AP - ANP: Approval for next process' and b.position = 2  and b.parent_block_version_checklist_template_id is NOT NULL; 
 
INSERT INTO ITEMS_VERSIONS_CHECKLISTS_TEMPLATES (version_checklist_template_id,position,title,item_type_id,block_version_checklist_template_id,option_field_version_checklist_template_id)  
select v.version_checklist_template_id, 1, 'Versão do checklist (não aplicável em caso de liberação em único fluxo)', 1, b.block_version_checklist_template_id, null  
from VERSIONS_CHECKLISTS_TEMPLATES v  
join checklists_templates c on v.checklist_template_id = c.checklist_template_id  
join BLOCKS_VERSIONS_CHECKLISTS_TEMPLATES b on v.version_checklist_template_id = b.version_checklist_template_id  
where c.description = 'Checklist A12 AP - ANP: Approval for next process' and b.position = 3 and b.parent_block_version_checklist_template_id is  NULL; 
 
INSERT INTO ITEMS_VERSIONS_CHECKLISTS_TEMPLATES (version_checklist_template_id,position,title,item_type_id,block_version_checklist_template_id,option_field_version_checklist_template_id)  
select v.version_checklist_template_id, 2, 'WFI/vapor limpo', 1, b.block_version_checklist_template_id, null  
from VERSIONS_CHECKLISTS_TEMPLATES v  
join checklists_templates c on v.checklist_template_id = c.checklist_template_id  
join BLOCKS_VERSIONS_CHECKLISTS_TEMPLATES b on v.version_checklist_template_id = b.version_checklist_template_id  
where c.description = 'Checklist A12 AP - ANP: Approval for next process' and b.position = 3 and b.parent_block_version_checklist_template_id is  NULL;
 
INSERT INTO ITEMS_VERSIONS_CHECKLISTS_TEMPLATES (version_checklist_template_id,position,title,item_type_id,block_version_checklist_template_id,option_field_version_checklist_template_id)  
select v.version_checklist_template_id, 3, 'Monitoramento ambiental', 1, b.block_version_checklist_template_id, null  
from VERSIONS_CHECKLISTS_TEMPLATES v  
join checklists_templates c on v.checklist_template_id = c.checklist_template_id  
join BLOCKS_VERSIONS_CHECKLISTS_TEMPLATES b on v.version_checklist_template_id = b.version_checklist_template_id  
where c.description = 'Checklist A12 AP - ANP: Approval for next process' and b.position =  3 and b.parent_block_version_checklist_template_id is  NULL;
 
INSERT INTO ITEMS_VERSIONS_CHECKLISTS_TEMPLATES (version_checklist_template_id,position,title,item_type_id,block_version_checklist_template_id,option_field_version_checklist_template_id)  
select v.version_checklist_template_id, 4, 'Desvios Maiores de EM', 1, b.block_version_checklist_template_id, null  
from VERSIONS_CHECKLISTS_TEMPLATES v  
join checklists_templates c on v.checklist_template_id = c.checklist_template_id  
join BLOCKS_VERSIONS_CHECKLISTS_TEMPLATES b on v.version_checklist_template_id = b.version_checklist_template_id  
where c.description = 'Checklist A12 AP - ANP: Approval for next process' and b.position =  3 and b.parent_block_version_checklist_template_id is  NULL;
 
INSERT INTO ITEMS_VERSIONS_CHECKLISTS_TEMPLATES (version_checklist_template_id,position,title,item_type_id,block_version_checklist_template_id,option_field_version_checklist_template_id)  
select v.version_checklist_template_id, 5, 'Resultados do teste de esterilidade disponível (contract. manuf.)', 1, b.block_version_checklist_template_id, o.option_field_version_checklist_template_id  
from VERSIONS_CHECKLISTS_TEMPLATES v  
join checklists_templates c on v.checklist_template_id = c.checklist_template_id  
join BLOCKS_VERSIONS_CHECKLISTS_TEMPLATES b on v.version_checklist_template_id = b.version_checklist_template_id  
join FIELDS_VERSIONS_CHECKLISTS_TEMPLATES f on v.version_checklist_template_id = f.version_checklist_template_id  
join OPTIONS_FIELDS_VERSIONS_CHECKLISTS_TEMPLATES o on f.field_version_checklist_template_id = o.field_version_checklist_template_id  
where c.description = 'Checklist A12 AP - ANP: Approval for next process' and b.position =  3 and b.parent_block_version_checklist_template_id is NULL and f.position = 8 and value = 2; 
 
INSERT INTO ITEMS_VERSIONS_CHECKLISTS_TEMPLATES (version_checklist_template_id,position,title,item_type_id,block_version_checklist_template_id,option_field_version_checklist_template_id)  
select v.version_checklist_template_id, 6, 'Disposição no LIMS (exportação direta)', 1, b.block_version_checklist_template_id, o.option_field_version_checklist_template_id  
from VERSIONS_CHECKLISTS_TEMPLATES v  
join checklists_templates c on v.checklist_template_id = c.checklist_template_id  
join BLOCKS_VERSIONS_CHECKLISTS_TEMPLATES b on v.version_checklist_template_id = b.version_checklist_template_id  
join FIELDS_VERSIONS_CHECKLISTS_TEMPLATES f on v.version_checklist_template_id = f.version_checklist_template_id  
join OPTIONS_FIELDS_VERSIONS_CHECKLISTS_TEMPLATES o on f.field_version_checklist_template_id = o.field_version_checklist_template_id  
where c.description = 'Checklist A12 AP - ANP: Approval for next process' and b.position =  3 and b.parent_block_version_checklist_template_id is  NULL and f.position = 8 and value = 1; 

INSERT INTO FIELDS_VERSIONS_CHECKLISTS_TEMPLATES (version_checklist_template_id, position, title, field_data_type_id, regex_validation, format, mandatory, is_key)  
select v.version_checklist_template_id, 1, 'Batch Number (Formulation)',1, '', null, 1, 1  
from VERSIONS_CHECKLISTS_TEMPLATES v  
join checklists_templates c on v.checklist_template_id = c.checklist_template_id  
join users u on v.creation_user_id = u.user_id and initials = '_adm' 
where c.description = 'Checklist A12 AP - FSA usando DBR'; 
 
INSERT INTO FIELDS_VERSIONS_CHECKLISTS_TEMPLATES (version_checklist_template_id, position, title, field_data_type_id, regex_validation, format, mandatory, is_key) 
select v.version_checklist_template_id, 2, 'Item number',2, null, null, 1, 0 
from VERSIONS_CHECKLISTS_TEMPLATES v 
join checklists_templates c on v.checklist_template_id = c.checklist_template_id 
cross join users u 
where c.description = 'Checklist A12 AP - FSA usando DBR' and initials = '_adm'; 
 
INSERT INTO FIELDS_VERSIONS_CHECKLISTS_TEMPLATES (version_checklist_template_id, position, title, field_data_type_id, regex_validation, format, mandatory, is_key) 
select v.version_checklist_template_id, 3, 'Batch Number (Uninspected)',1, '', null, 1, 0 
from VERSIONS_CHECKLISTS_TEMPLATES v 
join checklists_templates c on v.checklist_template_id = c.checklist_template_id 
cross join users u 
where c.description = 'Checklist A12 AP - FSA usando DBR' and initials = '_adm'; 
 
INSERT INTO FIELDS_VERSIONS_CHECKLISTS_TEMPLATES (version_checklist_template_id, position, title, field_data_type_id, regex_validation, format, mandatory, is_key) 
select v.version_checklist_template_id, 4, 'Item number',2, null, null, 1, 0 
from VERSIONS_CHECKLISTS_TEMPLATES v 
join checklists_templates c on v.checklist_template_id = c.checklist_template_id 
cross join users u 
where c.description = 'Checklist A12 AP - FSA usando DBR' and initials = '_adm'; 
 
INSERT INTO FIELDS_VERSIONS_CHECKLISTS_TEMPLATES (version_checklist_template_id, position, title, field_data_type_id, regex_validation, format, mandatory, is_key) 
select v.version_checklist_template_id, 5, 'Batch Number (Inspected)',1, '', null, 1, 0 
from VERSIONS_CHECKLISTS_TEMPLATES v 
join checklists_templates c on v.checklist_template_id = c.checklist_template_id 
cross join users u 
where c.description = 'Checklist A12 AP - FSA usando DBR' and initials = '_adm'; 
 
INSERT INTO FIELDS_VERSIONS_CHECKLISTS_TEMPLATES (version_checklist_template_id, position, title, field_data_type_id, regex_validation, format, mandatory, is_key) 
select v.version_checklist_template_id, 6, 'Item number',2, null, null, 1, 0 
from VERSIONS_CHECKLISTS_TEMPLATES v 
join checklists_templates c on v.checklist_template_id = c.checklist_template_id 
cross join users u 
where c.description = 'Checklist A12 AP - FSA usando DBR' and initials = '_adm'; 
 
INSERT INTO FIELDS_VERSIONS_CHECKLISTS_TEMPLATES (version_checklist_template_id, position, title, field_data_type_id, regex_validation, format, mandatory, is_key) 
select v.version_checklist_template_id, 7, 'Item name',1, null, null, 1, 0 
from VERSIONS_CHECKLISTS_TEMPLATES v 
join checklists_templates c on v.checklist_template_id = c.checklist_template_id 
cross join users u 
where c.description = 'Checklist A12 AP - FSA usando DBR' and initials = '_adm'; 
 
INSERT INTO BLOCKS_VERSIONS_CHECKLISTS_TEMPLATES (version_checklist_template_id, position, title)  
select v.version_checklist_template_id, 1, 'Parte 3: Final Status Assignment' 
from VERSIONS_CHECKLISTS_TEMPLATES v 
join checklists_templates c on v.checklist_template_id = c.checklist_template_id 
where c.description = 'Checklist A12 AP - FSA usando DBR'; 
 
INSERT INTO ITEMS_VERSIONS_CHECKLISTS_TEMPLATES (version_checklist_template_id,position,title,item_type_id,block_version_checklist_template_id,option_field_version_checklist_template_id) 
select v.version_checklist_template_id, 1, 'Versão do CheckList', 1, b.block_version_checklist_template_id, null 
from VERSIONS_CHECKLISTS_TEMPLATES v 
join checklists_templates c on v.checklist_template_id = c.checklist_template_id 
join BLOCKS_VERSIONS_CHECKLISTS_TEMPLATES b on v.version_checklist_template_id = b.version_checklist_template_id 
where c.description = 'Checklist A12 AP - FSA usando DBR' and b.position = 1; 
 
INSERT INTO ITEMS_VERSIONS_CHECKLISTS_TEMPLATES (version_checklist_template_id,position,title,item_type_id,block_version_checklist_template_id,option_field_version_checklist_template_id) 
select v.version_checklist_template_id, 2, 'Checklist partes 1 e 2 finalizadas', 1, b.block_version_checklist_template_id, null 
from VERSIONS_CHECKLISTS_TEMPLATES v 
join checklists_templates c on v.checklist_template_id = c.checklist_template_id 
join BLOCKS_VERSIONS_CHECKLISTS_TEMPLATES b on v.version_checklist_template_id = b.version_checklist_template_id 
where c.description = 'Checklist A12 AP - FSA usando DBR' and b.position = 1; 
 
INSERT INTO ITEMS_VERSIONS_CHECKLISTS_TEMPLATES (version_checklist_template_id,position,title,item_type_id,block_version_checklist_template_id,option_field_version_checklist_template_id) 
select v.version_checklist_template_id, 3, 'Fomrulário que contém informações com impacto em liberação de lote', 3, b.block_version_checklist_template_id, null 
from VERSIONS_CHECKLISTS_TEMPLATES v 
join checklists_templates c on v.checklist_template_id = c.checklist_template_id 
join BLOCKS_VERSIONS_CHECKLISTS_TEMPLATES b on v.version_checklist_template_id = b.version_checklist_template_id 
where c.description = 'Checklist A12 AP - FSA usando DBR' and b.position = 1; 
 
INSERT INTO ITEMS_VERSIONS_CHECKLISTS_TEMPLATES (version_checklist_template_id,position,title,item_type_id,block_version_checklist_template_id,option_field_version_checklist_template_id) 
select v.version_checklist_template_id, 4, 'APS (Aseptic Process Simulation)', 1, b.block_version_checklist_template_id, null 
from VERSIONS_CHECKLISTS_TEMPLATES v 
join checklists_templates c on v.checklist_template_id = c.checklist_template_id 
join BLOCKS_VERSIONS_CHECKLISTS_TEMPLATES b on v.version_checklist_template_id = b.version_checklist_template_id 
where c.description = 'Checklist A12 AP - FSA usando DBR' and b.position = 1; 
 
INSERT INTO ITEMS_VERSIONS_CHECKLISTS_TEMPLATES (version_checklist_template_id,position,title,item_type_id,block_version_checklist_template_id,option_field_version_checklist_template_id) 
select v.version_checklist_template_id, 5, 'PAS-X', 1, b.block_version_checklist_template_id, null 
from VERSIONS_CHECKLISTS_TEMPLATES v 
join checklists_templates c on v.checklist_template_id = c.checklist_template_id 
join BLOCKS_VERSIONS_CHECKLISTS_TEMPLATES b on v.version_checklist_template_id = b.version_checklist_template_id 
where c.description = 'Checklist A12 AP - FSA usando DBR' and b.position = 1; 
 
 


INSERT INTO FIELDS_VERSIONS_CHECKLISTS_TEMPLATES (version_checklist_template_id, position, title, field_data_type_id, regex_validation, format, mandatory, is_key) 
select v.version_checklist_template_id, 1, 'Batch Number',1, '', null, 1, 1 
from VERSIONS_CHECKLISTS_TEMPLATES v 
join checklists_templates c on v.checklist_template_id = c.checklist_template_id  
join users u on v.creation_user_id = u.user_id and initials = '_adm' 
where c.description = 'Checklist A12 AP - FSA: Bulk De STJ, CH e CL recebidos em MOC'; 
 
INSERT INTO FIELDS_VERSIONS_CHECKLISTS_TEMPLATES (version_checklist_template_id, position, title, field_data_type_id, regex_validation, format, mandatory, is_key) 
select v.version_checklist_template_id, 2, 'Item number',2, null, null, 1, 0 
from VERSIONS_CHECKLISTS_TEMPLATES v 
join checklists_templates c on v.checklist_template_id = c.checklist_template_id 
cross join users u 
where c.description = 'Checklist A12 AP - FSA: Bulk De STJ, CH e CL recebidos em MOC' and initials = '_adm'; 
 
INSERT INTO FIELDS_VERSIONS_CHECKLISTS_TEMPLATES (version_checklist_template_id, position, title, field_data_type_id, regex_validation, format, mandatory, is_key) 
select v.version_checklist_template_id, 3, 'Item name',1, null, null, 1, 0 
from VERSIONS_CHECKLISTS_TEMPLATES v 
join checklists_templates c on v.checklist_template_id = c.checklist_template_id 
cross join users u 
where c.description = 'Checklist A12 AP - FSA: Bulk De STJ, CH e CL recebidos em MOC' and initials = '_adm'; 
 

 
INSERT INTO ITEMS_VERSIONS_CHECKLISTS_TEMPLATES (version_checklist_template_id,position,title,item_type_id,block_version_checklist_template_id,option_field_version_checklist_template_id) 
select v.version_checklist_template_id, 1, 'Versão do CheckList', 1, b.block_version_checklist_template_id, null 
from VERSIONS_CHECKLISTS_TEMPLATES v 
join checklists_templates c on v.checklist_template_id = c.checklist_template_id 
join BLOCKS_VERSIONS_CHECKLISTS_TEMPLATES b on v.version_checklist_template_id = b.version_checklist_template_id 
where c.description = 'Checklist A12 AP - FSA: Bulk De STJ, CH e CL recebidos em MOC' and b.position = 1 and b.parent_block_version_checklist_template_id is null; 
 
INSERT INTO ITEMS_VERSIONS_CHECKLISTS_TEMPLATES (version_checklist_template_id,position,title,item_type_id,block_version_checklist_template_id,option_field_version_checklist_template_id) 
select v.version_checklist_template_id, 2, 'Consistência entre sistema e checklist', 1, b.block_version_checklist_template_id, null 
from VERSIONS_CHECKLISTS_TEMPLATES v 
join checklists_templates c on v.checklist_template_id = c.checklist_template_id 
join BLOCKS_VERSIONS_CHECKLISTS_TEMPLATES b on v.version_checklist_template_id = b.version_checklist_template_id 
where c.description = 'Checklist A12 AP - FSA: Bulk De STJ, CH e CL recebidos em MOC' and b.position = 1 and b.parent_block_version_checklist_template_id is null; 
 
INSERT INTO ITEMS_VERSIONS_CHECKLISTS_TEMPLATES (version_checklist_template_id,position,title,item_type_id,block_version_checklist_template_id,option_field_version_checklist_template_id) 
select v.version_checklist_template_id, 3, 'Liberação do lote no site produtor', 1, b.block_version_checklist_template_id, null 
from VERSIONS_CHECKLISTS_TEMPLATES v 
join checklists_templates c on v.checklist_template_id = c.checklist_template_id 
join BLOCKS_VERSIONS_CHECKLISTS_TEMPLATES b on v.version_checklist_template_id = b.version_checklist_template_id 
where c.description = 'Checklist A12 AP - FSA: Bulk De STJ, CH e CL recebidos em MOC' and b.position = 1 and b.parent_block_version_checklist_template_id is null; 
 
INSERT INTO ITEMS_VERSIONS_CHECKLISTS_TEMPLATES (version_checklist_template_id,position,title,item_type_id,block_version_checklist_template_id,option_field_version_checklist_template_id) 
select v.version_checklist_template_id, 4, 'Facility Monitoring System (FMS)', 1, b.block_version_checklist_template_id, null 
from VERSIONS_CHECKLISTS_TEMPLATES v 
join checklists_templates c on v.checklist_template_id = c.checklist_template_id 
join BLOCKS_VERSIONS_CHECKLISTS_TEMPLATES b on v.version_checklist_template_id = b.version_checklist_template_id 
where c.description = 'Checklist A12 AP - FSA: Bulk De STJ, CH e CL recebidos em MOC' and b.position = 1 and b.parent_block_version_checklist_template_id is null; 
 
INSERT INTO ITEMS_VERSIONS_CHECKLISTS_TEMPLATES (version_checklist_template_id,position,title,item_type_id,block_version_checklist_template_id,option_field_version_checklist_template_id) 
select v.version_checklist_template_id, 5, 'Amostras do LIMS', 1, b.block_version_checklist_template_id, null 
from VERSIONS_CHECKLISTS_TEMPLATES v 
join checklists_templates c on v.checklist_template_id = c.checklist_template_id 
join BLOCKS_VERSIONS_CHECKLISTS_TEMPLATES b on v.version_checklist_template_id = b.version_checklist_template_id 
where c.description = 'Checklist A12 AP - FSA: Bulk De STJ, CH e CL recebidos em MOC' and b.position = 1 and b.parent_block_version_checklist_template_id is null; 
 
INSERT INTO ITEMS_VERSIONS_CHECKLISTS_TEMPLATES (version_checklist_template_id,position,title,item_type_id,block_version_checklist_template_id,option_field_version_checklist_template_id) 
select v.version_checklist_template_id, 6, 'Temperatura durante transporte', 1, b.block_version_checklist_template_id, null 
from VERSIONS_CHECKLISTS_TEMPLATES v 
join checklists_templates c on v.checklist_template_id = c.checklist_template_id 
join BLOCKS_VERSIONS_CHECKLISTS_TEMPLATES b on v.version_checklist_template_id = b.version_checklist_template_id 
where c.description = 'Checklist A12 AP - FSA: Bulk De STJ, CH e CL recebidos em MOC' and b.position = 1 and b.parent_block_version_checklist_template_id is null; 
 
INSERT INTO ITEMS_VERSIONS_CHECKLISTS_TEMPLATES (version_checklist_template_id,position,title,item_type_id,block_version_checklist_template_id,option_field_version_checklist_template_id) 
select v.version_checklist_template_id, 7, 'NNTZW', 1, b.block_version_checklist_template_id, null 
from VERSIONS_CHECKLISTS_TEMPLATES v 
join checklists_templates c on v.checklist_template_id = c.checklist_template_id 
join BLOCKS_VERSIONS_CHECKLISTS_TEMPLATES b on v.version_checklist_template_id = b.version_checklist_template_id 
where c.description = 'Checklist A12 AP - FSA: Bulk De STJ, CH e CL recebidos em MOC' and b.position = 1 and b.parent_block_version_checklist_template_id is null; 
 
INSERT INTO ITEMS_VERSIONS_CHECKLISTS_TEMPLATES (version_checklist_template_id, position, title, item_type_id, block_version_checklist_template_id, option_field_version_checklist_template_id, options_title) 
select v.version_checklist_template_id, 8, 'Informações críticas no SAP', 4, b.block_version_checklist_template_id, null, 'Informações corrigidas/transferidas manualmente?' 
from VERSIONS_CHECKLISTS_TEMPLATES v 
join checklists_templates c on v.checklist_template_id = c.checklist_template_id 
join BLOCKS_VERSIONS_CHECKLISTS_TEMPLATES b on v.version_checklist_template_id = b.version_checklist_template_id 
where c.description = 'Checklist A12 AP - FSA: Bulk De STJ, CH e CL recebidos em MOC' and b.position = 1 and b.parent_block_version_checklist_template_id is null; 
 
INSERT INTO OPTIONS_ITEMS_VERSIONS_CHECKLISTS_TEMPLATES (item_version_checklist_template_id, title, value) 
select i.item_version_checklist_template_id, 'Manufacturing date/expiry date', 1 
from VERSIONS_CHECKLISTS_TEMPLATES v 
join checklists_templates c on v.checklist_template_id = c.checklist_template_id 
join ITEMS_VERSIONS_CHECKLISTS_TEMPLATES i on v.version_checklist_template_id = i.version_checklist_template_id 
where c.description = 'Checklist A12 AP - FSA: Bulk De STJ, CH e CL recebidos em MOC' and i.position = 8; 
 
INSERT INTO OPTIONS_ITEMS_VERSIONS_CHECKLISTS_TEMPLATES (item_version_checklist_template_id, title, value) 
select i.item_version_checklist_template_id, 'QA notes', 2 
from VERSIONS_CHECKLISTS_TEMPLATES v 
join checklists_templates c on v.checklist_template_id = c.checklist_template_id 
join ITEMS_VERSIONS_CHECKLISTS_TEMPLATES i on v.version_checklist_template_id = i.version_checklist_template_id 
where c.description = 'Checklist A12 AP - FSA: Bulk De STJ, CH e CL recebidos em MOC' and i.position = 8; 
 
INSERT INTO OPTIONS_ITEMS_VERSIONS_CHECKLISTS_TEMPLATES (item_version_checklist_template_id, title, value) 
select i.item_version_checklist_template_id, 'Batch classification', 3 
from VERSIONS_CHECKLISTS_TEMPLATES v 
join checklists_templates c on v.checklist_template_id = c.checklist_template_id 
join ITEMS_VERSIONS_CHECKLISTS_TEMPLATES i on v.version_checklist_template_id = i.version_checklist_template_id 
where c.description = 'Checklist A12 AP - FSA: Bulk De STJ, CH e CL recebidos em MOC' and i.position = 8; 
 
INSERT INTO ITEMS_VERSIONS_CHECKLISTS_TEMPLATES (version_checklist_template_id, position, title, item_type_id, block_version_checklist_template_id, option_field_version_checklist_template_id, options_title) 
select v.version_checklist_template_id, 9, 'Desvios', 3, b.block_version_checklist_template_id, null, 'Contém limitação?' 
from VERSIONS_CHECKLISTS_TEMPLATES v 
join checklists_templates c on v.checklist_template_id = c.checklist_template_id 
join BLOCKS_VERSIONS_CHECKLISTS_TEMPLATES b on v.version_checklist_template_id = b.version_checklist_template_id 
where c.description = 'Checklist A12 AP - FSA: Bulk De STJ, CH e CL recebidos em MOC' and b.position = 1 and b.parent_block_version_checklist_template_id is null; 
 
INSERT INTO OPTIONS_ITEMS_VERSIONS_CHECKLISTS_TEMPLATES (item_version_checklist_template_id, title, value) 
select i.item_version_checklist_template_id, 'Sim', 1 
from VERSIONS_CHECKLISTS_TEMPLATES v 
join checklists_templates c on v.checklist_template_id = c.checklist_template_id 
join ITEMS_VERSIONS_CHECKLISTS_TEMPLATES i on v.version_checklist_template_id = i.version_checklist_template_id 
where c.description = 'Checklist A12 AP - FSA: Bulk De STJ, CH e CL recebidos em MOC' and i.position = 9; 
 
INSERT INTO OPTIONS_ITEMS_VERSIONS_CHECKLISTS_TEMPLATES (item_version_checklist_template_id, title, value) 
select i.item_version_checklist_template_id, 'Não', 2 
from VERSIONS_CHECKLISTS_TEMPLATES v 
join checklists_templates c on v.checklist_template_id = c.checklist_template_id 
join ITEMS_VERSIONS_CHECKLISTS_TEMPLATES i on v.version_checklist_template_id = i.version_checklist_template_id 
where c.description = 'Checklist A12 AP - FSA: Bulk De STJ, CH e CL recebidos em MOC' and i.position = 9; 
 
INSERT INTO ITEMS_VERSIONS_CHECKLISTS_TEMPLATES (version_checklist_template_id,position,title,item_type_id,block_version_checklist_template_id,option_field_version_checklist_template_id) 
select v.version_checklist_template_id, 10, 'Desvios abertos', 1, b.block_version_checklist_template_id, null 
from VERSIONS_CHECKLISTS_TEMPLATES v 
join checklists_templates c on v.checklist_template_id = c.checklist_template_id 
join BLOCKS_VERSIONS_CHECKLISTS_TEMPLATES b on v.version_checklist_template_id = b.version_checklist_template_id 
where c.description = 'Checklist A12 AP - FSA: Bulk De STJ, CH e CL recebidos em MOC' and b.position = 1 and b.parent_block_version_checklist_template_id is null; 
 
INSERT INTO ITEMS_VERSIONS_CHECKLISTS_TEMPLATES (version_checklist_template_id,position,title,item_type_id,block_version_checklist_template_id,option_field_version_checklist_template_id) 
select v.version_checklist_template_id, 11, 'CRs no novoGlow', 1, b.block_version_checklist_template_id, null 
from VERSIONS_CHECKLISTS_TEMPLATES v 
join checklists_templates c on v.checklist_template_id = c.checklist_template_id 
join BLOCKS_VERSIONS_CHECKLISTS_TEMPLATES b on v.version_checklist_template_id = b.version_checklist_template_id 
where c.description = 'Checklist A12 AP - FSA: Bulk De STJ, CH e CL recebidos em MOC' and b.position = 1 and b.parent_block_version_checklist_template_id is null; 
 
INSERT INTO ITEMS_VERSIONS_CHECKLISTS_TEMPLATES (version_checklist_template_id,position,title,item_type_id,block_version_checklist_template_id,option_field_version_checklist_template_id) 
select v.version_checklist_template_id, 12, 'Formulários que contém informações com impacto em liberação de lote', 1, b.block_version_checklist_template_id, null 
from VERSIONS_CHECKLISTS_TEMPLATES v 
join checklists_templates c on v.checklist_template_id = c.checklist_template_id 
join BLOCKS_VERSIONS_CHECKLISTS_TEMPLATES b on v.version_checklist_template_id = b.version_checklist_template_id 
where c.description = 'Checklist A12 AP - FSA: Bulk De STJ, CH e CL recebidos em MOC' and b.position = 1 and b.parent_block_version_checklist_template_id is null; 
 
 
INSERT INTO ITEMS_VERSIONS_CHECKLISTS_TEMPLATES (version_checklist_template_id, position, title, item_type_id, block_version_checklist_template_id, option_field_version_checklist_template_id, options_title) 
select v.version_checklist_template_id, 1, 'Final Status Assignment in SAP', 3, b.block_version_checklist_template_id, null, '"I hereby confirm that the manufacture and control of the batch have been carried out in full compliance with the GMP requirements of the EU and local Regulatory Authority and with the requirements of the Marketing Authorisation".' 
from VERSIONS_CHECKLISTS_TEMPLATES v 
join checklists_templates c on v.checklist_template_id = c.checklist_template_id 
join BLOCKS_VERSIONS_CHECKLISTS_TEMPLATES b on v.version_checklist_template_id = b.version_checklist_template_id 
where c.description = 'Checklist A12 AP - FSA: Bulk De STJ, CH e CL recebidos em MOC' and b.position = 2 and b.parent_block_version_checklist_template_id is null; 
 
INSERT INTO OPTIONS_ITEMS_VERSIONS_CHECKLISTS_TEMPLATES (item_version_checklist_template_id, title, value) 
select i.item_version_checklist_template_id, 'A,approved', 1 
from VERSIONS_CHECKLISTS_TEMPLATES v 
join checklists_templates c on v.checklist_template_id = c.checklist_template_id 
join BLOCKS_VERSIONS_CHECKLISTS_TEMPLATES b on v.version_checklist_template_id = b.version_checklist_template_id 
join ITEMS_VERSIONS_CHECKLISTS_TEMPLATES i on v.version_checklist_template_id = i.version_checklist_template_id and i.block_version_checklist_template_id = b.block_version_checklist_template_id 
where c.description = 'Checklist A12 AP - FSA: Bulk De STJ, CH e CL recebidos em MOC' and i.position = 1 and b.position = 2 and b.parent_block_version_checklist_template_id is null; 
 
INSERT INTO OPTIONS_ITEMS_VERSIONS_CHECKLISTS_TEMPLATES (item_version_checklist_template_id, title, value) 
select i.item_version_checklist_template_id, 'L,limited', 2 
from VERSIONS_CHECKLISTS_TEMPLATES v 
join checklists_templates c on v.checklist_template_id = c.checklist_template_id 
join BLOCKS_VERSIONS_CHECKLISTS_TEMPLATES b on v.version_checklist_template_id = b.version_checklist_template_id 
join ITEMS_VERSIONS_CHECKLISTS_TEMPLATES i on v.version_checklist_template_id = i.version_checklist_template_id and i.block_version_checklist_template_id = b.block_version_checklist_template_id 
where c.description = 'Checklist A12 AP - FSA: Bulk De STJ, CH e CL recebidos em MOC' and i.position = 1 and b.position = 2 and b.parent_block_version_checklist_template_id is null; 
 
INSERT INTO OPTIONS_ITEMS_VERSIONS_CHECKLISTS_TEMPLATES (item_version_checklist_template_id, title, value) 
select i.item_version_checklist_template_id, 'R,rejected', 3 
from VERSIONS_CHECKLISTS_TEMPLATES v 
join checklists_templates c on v.checklist_template_id = c.checklist_template_id 
join BLOCKS_VERSIONS_CHECKLISTS_TEMPLATES b on v.version_checklist_template_id = b.version_checklist_template_id 
join ITEMS_VERSIONS_CHECKLISTS_TEMPLATES i on v.version_checklist_template_id = i.version_checklist_template_id and i.block_version_checklist_template_id = b.block_version_checklist_template_id 
where c.description = 'Checklist A12 AP - FSA: Bulk De STJ, CH e CL recebidos em MOC' and i.position = 1 and b.position = 2 and b.parent_block_version_checklist_template_id is null; 
 
INSERT INTO OPTIONS_ITEMS_VERSIONS_CHECKLISTS_TEMPLATES (item_version_checklist_template_id, title, value) 
select i.item_version_checklist_template_id, 'T,technical', 4 
from VERSIONS_CHECKLISTS_TEMPLATES v 
join checklists_templates c on v.checklist_template_id = c.checklist_template_id 
join BLOCKS_VERSIONS_CHECKLISTS_TEMPLATES b on v.version_checklist_template_id = b.version_checklist_template_id 
join ITEMS_VERSIONS_CHECKLISTS_TEMPLATES i on v.version_checklist_template_id = i.version_checklist_template_id and i.block_version_checklist_template_id = b.block_version_checklist_template_id 
where c.description = 'Checklist A12 AP - FSA: Bulk De STJ, CH e CL recebidos em MOC' and i.position = 1 and b.position = 2 and b.parent_block_version_checklist_template_id is null; 
 
INSERT INTO ITEMS_VERSIONS_CHECKLISTS_TEMPLATES (version_checklist_template_id,position,title,item_type_id,block_version_checklist_template_id,option_field_version_checklist_template_id) 
select v.version_checklist_template_id, 1, 'Status assignment correto no SAP', 1, b.block_version_checklist_template_id, null 
from VERSIONS_CHECKLISTS_TEMPLATES v 
join checklists_templates c on v.checklist_template_id = c.checklist_template_id 
join BLOCKS_VERSIONS_CHECKLISTS_TEMPLATES b on v.version_checklist_template_id = b.version_checklist_template_id 
where c.description = 'Checklist A12 AP - FSA: Bulk De STJ, CH e CL recebidos em MOC' and b.position = 2 and b.parent_block_version_checklist_template_id is not null; 
 
INSERT INTO ITEMS_VERSIONS_CHECKLISTS_TEMPLATES (version_checklist_template_id,position,title,item_type_id,block_version_checklist_template_id,option_field_version_checklist_template_id) 
select v.version_checklist_template_id, 2, 'Informações críticas no SAP (Se alteradas)', 1, b.block_version_checklist_template_id, null 
from VERSIONS_CHECKLISTS_TEMPLATES v 
join checklists_templates c on v.checklist_template_id = c.checklist_template_id 
join BLOCKS_VERSIONS_CHECKLISTS_TEMPLATES b on v.version_checklist_template_id = b.version_checklist_template_id 
where c.description = 'Checklist A12 AP - FSA: Bulk De STJ, CH e CL recebidos em MOC' and b.position = 2 and b.parent_block_version_checklist_template_id is not null;  

INSERT INTO DEPENDENCIES_ITEMS_VERSIONS_CHECKLISTS_TEMPLATES (item_version_checklist_template_id,dependent_block_version_checklist_template_id,dependent_item_version_checklist_template_id,dependent_version_checklist_template_id)  
select sum(t.a), null,null,  sum(t.b) 
from (  
select b.item_version_checklist_template_id as a, 0 as b  
from VERSIONS_CHECKLISTS_TEMPLATES v  
join checklists_templates c on v.checklist_template_id = c.checklist_template_id  
join ITEMS_VERSIONS_CHECKLISTS_TEMPLATES b on v.version_checklist_template_id = b.version_checklist_template_id  
where c.description = 'Checklist A12 AP - FSA usando DBR' and b.position = 2 
union   
select 0 as a, v.version_checklist_template_id as b  
from VERSIONS_CHECKLISTS_TEMPLATES v  
join checklists_templates c on v.checklist_template_id = c.checklist_template_id  
where c.description = 'Checklist A12 AP - ANP: Approval for next process' 
) t; 
 
INSERT INTO DEPENDENCIES_ITEMS_VERSIONS_CHECKLISTS_TEMPLATES (item_version_checklist_template_id,dependent_block_version_checklist_template_id,dependent_item_version_checklist_template_id,dependent_version_checklist_template_id)  
select sum(t.a),  sum(t.b) , null,null 
from (  
select b.block_version_checklist_template_id as a, 0 as b  
from VERSIONS_CHECKLISTS_TEMPLATES v  
join checklists_templates c on v.checklist_template_id = c.checklist_template_id  
join BLOCKS_VERSIONS_CHECKLISTS_TEMPLATES b on v.version_checklist_template_id = b.version_checklist_template_id  
where c.description = 'Checklist A12 AP - ANP: Approval for next process' and (b.position = 2 and b.parent_block_version_checklist_template_id is NULL) 
union   
select 0 as a, b.block_version_checklist_template_id as b 
from VERSIONS_CHECKLISTS_TEMPLATES v 
join checklists_templates c on v.checklist_template_id = c.checklist_template_id 
join BLOCKS_VERSIONS_CHECKLISTS_TEMPLATES b on v.version_checklist_template_id = b.version_checklist_template_id 
where c.description = 'Checklist A12 AP - ANP: Approval for next process' and (b.position = 1 and b.parent_block_version_checklist_template_id is NULL)
) t; 
 
INSERT INTO DEPENDENCIES_BLOCKS_VERSIONS_CHECKLISTS_TEMPLATES (block_version_checklist_template_id,dependent_block_version_checklist_template_id,dependent_item_version_checklist_template_id, dependent_version_checklist_template_id) 
select sum(t.a), null, sum(t.b), null 
from ( 
select b.block_version_checklist_template_id as a, 0 as b 
from VERSIONS_CHECKLISTS_TEMPLATES v 
join checklists_templates c on v.checklist_template_id = c.checklist_template_id 
join BLOCKS_VERSIONS_CHECKLISTS_TEMPLATES b on v.version_checklist_template_id = b.version_checklist_template_id 
where c.description = 'Checklist A12 AP - ANP: Approval for next process' and b.position = 2 and b.parent_block_version_checklist_template_id is NOT NULL 
union  
select 0 as a, b.item_version_checklist_template_id as b  
from VERSIONS_CHECKLISTS_TEMPLATES v  
join checklists_templates c on v.checklist_template_id = c.checklist_template_id  
join ITEMS_VERSIONS_CHECKLISTS_TEMPLATES b on v.version_checklist_template_id = b.version_checklist_template_id  
where c.description = 'Checklist A12 AP - ANP: Approval for next process' and b.position = 14  
) t; 
 
