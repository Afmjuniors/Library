import React, { useState } from 'react';
import {
  View,
  Text,
  TextInput,
  TouchableOpacity,
  StyleSheet,
  Modal,
  ScrollView,
  Alert,
} from 'react-native';
import { Picker } from '@react-native-picker/picker';
import { COLORS, SPACING, FONT_SIZES, FONT_WEIGHTS, BORDER_RADIUS, SHADOWS } from '../constants';
import { OrganizationRules } from '../types';

interface OrganizationFormData {
  name: string;
  description: string;
  image: string | null;
  rules: OrganizationRules;
}

interface OrganizationFormProps {
  isVisible: boolean;
  onClose: () => void;
  onSubmit: (data: OrganizationFormData) => Promise<void>;
  isLoading?: boolean;
}

export const OrganizationForm: React.FC<OrganizationFormProps> = ({
  isVisible,
  onClose,
  onSubmit,
  isLoading = false,
}) => {
  const [formData, setFormData] = useState<OrganizationFormData>({
    name: '',
    description: '',
    image: null,
         rules: {
       loanDurationDays: 30,
       meetingFrequency: 'weekly',
       meetingDay: 'monday',
       meetingTime: '19:00',
       nextWeekMeeting: false, // Novo campo para quinzenal
     },
  });

  const frequencyOptions = [
    { value: 'na', label: 'Não há reuniões' },
    { value: 'daily', label: 'Diária' },
    { value: 'weekly', label: 'Semanal' },
    { value: 'biweekly', label: 'Quinzenal' },
    { value: 'monthly', label: 'Mensal' },
  ];

  const dayOptions = [
    { value: 'monday', label: 'Segunda-feira' },
    { value: 'tuesday', label: 'Terça-feira' },
    { value: 'wednesday', label: 'Quarta-feira' },
    { value: 'thursday', label: 'Quinta-feira' },
    { value: 'friday', label: 'Sexta-feira' },
    { value: 'saturday', label: 'Sábado' },
    { value: 'sunday', label: 'Domingo' },
  ];

  const shouldShowDayPicker = () => {
    return formData.rules.meetingFrequency === 'weekly' || 
           formData.rules.meetingFrequency === 'biweekly' || 
           formData.rules.meetingFrequency === 'monthly';
  };

  const handleSubmit = async () => {
    if (!formData.name.trim()) {
      Alert.alert('Erro', 'Nome da organização é obrigatório');
      return;
    }

    if (!formData.description.trim()) {
      Alert.alert('Erro', 'Descrição da organização é obrigatória');
      return;
    }

    try {
      await onSubmit(formData);
      handleClose();
    } catch (error) {
      console.error('Erro ao criar organização:', error);
    }
  };

  const handleClose = () => {
         setFormData({
       name: '',
       description: '',
       image: null,
                rules: {
           loanDurationDays: 30,
           meetingFrequency: 'weekly',
           meetingDay: 'monday',
           meetingTime: '19:00',
           nextWeekMeeting: false,
         },
     });
    onClose();
  };

  return (
    <Modal
      visible={isVisible}
      animationType="slide"
      transparent={true}
      onRequestClose={handleClose}
    >
      <View style={styles.overlay}>
        <View style={styles.modalContainer}>
          <View style={styles.header}>
            <Text style={styles.title}>Criar Nova Organização</Text>
            <TouchableOpacity onPress={handleClose} style={styles.closeButton}>
              <Text style={styles.closeButtonText}>✕</Text>
            </TouchableOpacity>
          </View>

          <ScrollView style={styles.content} showsVerticalScrollIndicator={false}>
            <View style={styles.formSection}>
              <Text style={styles.sectionTitle}>Informações Básicas</Text>
              
              <View style={styles.inputGroup}>
                <Text style={styles.label}>Nome da Organização *</Text>
                <TextInput
                  style={styles.input}
                  value={formData.name}
                  onChangeText={(text) => setFormData(prev => ({ ...prev, name: text }))}
                  placeholder="Digite o nome da organização"
                  placeholderTextColor={COLORS.gray[400]}
                />
              </View>

              <View style={styles.inputGroup}>
                <Text style={styles.label}>Descrição *</Text>
                <TextInput
                  style={[styles.input, styles.textArea]}
                  value={formData.description}
                  onChangeText={(text) => setFormData(prev => ({ ...prev, description: text }))}
                  placeholder="Descreva a organização e seus objetivos"
                  placeholderTextColor={COLORS.gray[400]}
                  multiline
                  numberOfLines={4}
                  textAlignVertical="top"
                />
              </View>

              <View style={styles.inputGroup}>
                <Text style={styles.label}>URL da Imagem (opcional)</Text>
                <TextInput
                  style={styles.input}
                  value={formData.image || ''}
                  onChangeText={(text) => setFormData(prev => ({ ...prev, image: text || null }))}
                  placeholder="https://exemplo.com/imagem.jpg"
                  placeholderTextColor={COLORS.gray[400]}
                />
              </View>
            </View>

            <View style={styles.formSection}>
              <Text style={styles.sectionTitle}>Regras da Organização</Text>
              
              <View style={styles.rulesContainer}>
                <View style={styles.ruleItem}>
                  <Text style={styles.ruleLabel}>Duração do empréstimo (dias)</Text>
                  <TextInput
                    style={styles.ruleInput}
                    value={formData.rules.loanDurationDays.toString()}
                    onChangeText={(text) => {
                      const value = parseInt(text) || 30;
                      setFormData(prev => ({
                        ...prev,
                        rules: { ...prev.rules, loanDurationDays: value }
                      }));
                    }}
                    keyboardType="numeric"
                    placeholder="30"
                  />
                </View>

                <View style={styles.ruleItem}>
                  <Text style={styles.ruleLabel}>Frequência de reuniões</Text>
                                    <View style={styles.pickerContainer}>
                                         <Picker
                       selectedValue={formData.rules.meetingFrequency}
                       style={styles.picker}
                       mode="dropdown"
                       onValueChange={(itemValue) => {
                        setFormData(prev => ({
                          ...prev,
                          rules: { 
                            ...prev.rules, 
                            meetingFrequency: itemValue,
                            // Reset meetingDay if frequency is 'na' or 'daily'
                            meetingDay: (itemValue === 'na' || itemValue === 'daily') 
                              ? undefined 
                              : prev.rules.meetingDay
                          }
                        }));
                      }}
                    >
                      {frequencyOptions.map((option) => (
                        <Picker.Item 
                          key={option.value} 
                          label={option.label} 
                          value={option.value} 
                        />
                      ))}
                    </Picker>
                  </View>
                </View>

                {shouldShowDayPicker() && (
                  <View style={styles.ruleItem}>
                    <Text style={styles.ruleLabel}>Dia da semana</Text>
                                        <View style={styles.pickerContainer}>
                                             <Picker
                         selectedValue={formData.rules.meetingDay}
                         style={styles.picker}
                         mode="dropdown"
                         onValueChange={(itemValue) => {
                          setFormData(prev => ({
                            ...prev,
                            rules: { ...prev.rules, meetingDay: itemValue }
                          }));
                        }}
                      >
                        {dayOptions.map((option) => (
                          <Picker.Item 
                            key={option.value} 
                            label={option.label} 
                            value={option.value} 
                          />
                        ))}
                      </Picker>
                    </View>
                  </View>
                )}

                <View style={styles.ruleItem}>
                  <Text style={styles.ruleLabel}>Horário da reunião</Text>
                  <TextInput
                    style={styles.ruleInput}
                    value={formData.rules.meetingTime || ''}
                    onChangeText={(text) => {
                      setFormData(prev => ({
                        ...prev,
                        rules: { ...prev.rules, meetingTime: text }
                      }));
                    }}
                    placeholder="19:00"
                  />
                </View>

                

                 {formData.rules.meetingFrequency === 'biweekly' && (
                   <View style={styles.ruleItem}>
                     <Text style={styles.ruleLabel}>Próxima semana terá encontro</Text>
                     <TouchableOpacity
                       style={[styles.toggleButton, formData.rules.nextWeekMeeting && styles.toggleButtonActive]}
                       onPress={() => {
                         setFormData(prev => ({
                           ...prev,
                           rules: { ...prev.rules, nextWeekMeeting: !prev.rules.nextWeekMeeting }
                         }));
                       }}
                     >
                       <Text style={styles.toggleText}>
                         {formData.rules.nextWeekMeeting ? 'Sim' : 'Não'}
                       </Text>
                     </TouchableOpacity>
                   </View>
                 )}
              </View>
            </View>
          </ScrollView>

          <View style={styles.footer}>
            <TouchableOpacity
              style={[styles.cancelButton, styles.footerButton]}
              onPress={handleClose}
              disabled={isLoading}
            >
              <Text style={styles.cancelButtonText}>Cancelar</Text>
            </TouchableOpacity>
            
            <TouchableOpacity
              style={[styles.submitButton, styles.footerButton, isLoading && styles.disabledButton]}
              onPress={handleSubmit}
              disabled={isLoading}
            >
              <Text style={styles.submitButtonText}>
                {isLoading ? 'Criando...' : 'Criar Organização'}
              </Text>
            </TouchableOpacity>
          </View>
        </View>
      </View>
    </Modal>
  );
};

const styles = StyleSheet.create({
  overlay: {
    flex: 1,
    backgroundColor: 'rgba(0, 0, 0, 0.5)',
    justifyContent: 'center',
    alignItems: 'center',
  },
  modalContainer: {
    backgroundColor: COLORS.white,
    borderRadius: BORDER_RADIUS.lg,
    width: '90%',
    maxWidth: 500,
    maxHeight: '80%',
    ...SHADOWS.large,
  },
  header: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    padding: SPACING.lg,
    borderBottomWidth: 1,
    borderBottomColor: COLORS.gray[200],
  },
  title: {
    fontSize: FONT_SIZES.xl,
    fontWeight: FONT_WEIGHTS.bold,
    color: COLORS.text.primary,
  },
  closeButton: {
    width: 32,
    height: 32,
    borderRadius: 16,
    backgroundColor: COLORS.gray[200],
    justifyContent: 'center',
    alignItems: 'center',
  },
  closeButtonText: {
    fontSize: FONT_SIZES.md,
    color: COLORS.text.secondary,
  },
  content: {
    flex: 1,
    padding: SPACING.lg,
  },
  formSection: {
    marginBottom: SPACING.xl,
  },
  sectionTitle: {
    fontSize: FONT_SIZES.lg,
    fontWeight: FONT_WEIGHTS.semibold,
    color: COLORS.text.primary,
    marginBottom: SPACING.md,
  },
  inputGroup: {
    marginBottom: SPACING.md,
  },
  label: {
    fontSize: FONT_SIZES.sm,
    fontWeight: FONT_WEIGHTS.medium,
    color: COLORS.text.primary,
    marginBottom: SPACING.xs,
  },
  input: {
    borderWidth: 1,
    borderColor: COLORS.gray[300],
    borderRadius: BORDER_RADIUS.md,
    padding: SPACING.md,
    fontSize: FONT_SIZES.md,
    color: COLORS.text.primary,
    backgroundColor: COLORS.white,
  },
  textArea: {
    height: 100,
    textAlignVertical: 'top',
  },
  rulesContainer: {
    gap: SPACING.md,
  },
  ruleItem: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    paddingVertical: SPACING.sm,
    borderBottomWidth: 1,
    borderBottomColor: COLORS.gray[100],
  },
  ruleLabel: {
    fontSize: FONT_SIZES.sm,
    color: COLORS.text.secondary,
    flex: 1,
  },
  ruleInput: {
    borderWidth: 1,
    borderColor: COLORS.gray[300],
    borderRadius: BORDER_RADIUS.sm,
    padding: SPACING.sm,
    fontSize: FONT_SIZES.sm,
    color: COLORS.text.primary,
    backgroundColor: COLORS.white,
    width: 80,
    textAlign: 'center',
  },
  pickerContainer: {
    borderWidth: 1,
    borderColor: COLORS.gray[300],
    borderRadius: BORDER_RADIUS.sm,
    backgroundColor: COLORS.white,
    minWidth: 120,
    paddingHorizontal: SPACING.sm,
    height: 40,
    justifyContent: 'center',
  },
  picker: {
    height: 40,
    color: COLORS.text.primary,
    fontSize: FONT_SIZES.sm,
    borderWidth: 0,
  },
  footer: {
    flexDirection: 'row',
    padding: SPACING.lg,
    borderTopWidth: 1,
    borderTopColor: COLORS.gray[200],
    gap: SPACING.md,
  },
  footerButton: {
    flex: 1,
    paddingVertical: SPACING.md,
    borderRadius: BORDER_RADIUS.md,
    alignItems: 'center',
  },
  cancelButton: {
    backgroundColor: COLORS.gray[200],
  },
  cancelButtonText: {
    color: COLORS.text.secondary,
    fontSize: FONT_SIZES.md,
    fontWeight: FONT_WEIGHTS.semibold,
  },
  submitButton: {
    backgroundColor: COLORS.primary,
  },
  submitButtonText: {
    color: COLORS.white,
    fontSize: FONT_SIZES.md,
    fontWeight: FONT_WEIGHTS.semibold,
  },
  disabledButton: {
    opacity: 0.6,
  },
  toggleButton: {
    borderWidth: 1,
    borderColor: COLORS.gray[300],
    borderRadius: BORDER_RADIUS.sm,
    padding: SPACING.sm,
    backgroundColor: COLORS.gray[100],
    width: 80,
    alignItems: 'center',
  },
  toggleButtonActive: {
    backgroundColor: COLORS.primary,
    borderColor: COLORS.primary,
  },
  toggleText: {
    fontSize: FONT_SIZES.sm,
    color: COLORS.text.secondary,
    fontWeight: FONT_WEIGHTS.semibold,
  },
}); 