import { useState, useCallback, useEffect } from 'react';
import { Alert } from 'react-native';
import { ExtendedOrganization, Organization, OrganizationRules } from '../types';
import organizationService from '../services/organizationService';

interface CreateOrganizationData {
  name: string;
  description: string;
  image: string | null;
  rules: OrganizationRules;
}

export const useOrganizations = () => {
  const [organizations, setOrganizations] = useState<ExtendedOrganization[]>([]);
  const [selectedOrganization, setSelectedOrganization] = useState<ExtendedOrganization | null>(null);
  const [isLoading, setIsLoading] = useState(true);

  // Carregar organizações da API
  useEffect(() => {
    const loadOrganizations = async () => {
      try {
        setIsLoading(true);
        console.log('🏢 Carregando organizações da API...');
        
        const organizationsData = await organizationService.getOrganizations();
        console.log('📥 Organizações carregadas:', organizationsData.length);
        
        setOrganizations(organizationsData);
        console.log('✅ Organizações carregadas com sucesso');
      } catch (error) {
        console.error('❌ Erro ao carregar organizações da API:', error);
        Alert.alert('Erro', 'Falha ao carregar organizações. Tente novamente.');
        setOrganizations([]);
      } finally {
        setIsLoading(false);
      }
    };

    loadOrganizations();
  }, []);

  const createOrganization = useCallback(async (data: CreateOrganizationData, creatorId: number) => {
    if (!data.name.trim()) {
      Alert.alert('Erro', 'Nome da organização é obrigatório');
      return null;
    }

    if (!data.description.trim()) {
      Alert.alert('Erro', 'Descrição da organização é obrigatória');
      return null;
    }

    try {
      console.log('🏢 Criando organização na API...');
      
      const orgData = {
        name: data.name,
        description: data.description,
        image: data.image || undefined,
        rules: data.rules,
      };

      const newOrganization = await organizationService.createOrganization(orgData);
      
      console.log('📥 Organização criada:', newOrganization);
      
      // Atualizar lista local
      setOrganizations(prev => [...prev, newOrganization]);
      
      Alert.alert('Sucesso', 'Organização criada com sucesso!');
      return newOrganization;
    } catch (error) {
      console.error('❌ Erro ao criar organização na API:', error);
      Alert.alert('Erro', 'Falha ao criar organização. Tente novamente.');
      return null;
    }
  }, []);

  const updateOrganization = useCallback(async (orgId: number, updates: Partial<ExtendedOrganization>) => {
    try {
      console.log('🏢 Atualizando organização na API...');
      
      const updatedOrganization = await organizationService.updateOrganization({
        organizationId: orgId,
        ...updates,
      });
      
      console.log('📥 Organização atualizada:', updatedOrganization);
      
      // Atualizar lista local
      setOrganizations(prev => prev.map(org => 
        org.id === orgId ? updatedOrganization : org
      ));
      
      Alert.alert('Sucesso', 'Organização atualizada com sucesso!');
    } catch (error) {
      console.error('❌ Erro ao atualizar organização na API:', error);
      Alert.alert('Erro', 'Falha ao atualizar organização. Tente novamente.');
    }
  }, []);

  const deleteOrganization = useCallback(async (orgId: number) => {
    try {
      console.log('🏢 Excluindo organização na API...');
      
      await organizationService.deleteOrganization(orgId);
      
      console.log('✅ Organização excluída com sucesso');
      
      // Atualizar lista local
      setOrganizations(prev => prev.filter(org => org.id !== orgId));
      
      Alert.alert('Sucesso', 'Organização excluída com sucesso!');
    } catch (error) {
      console.error('❌ Erro ao excluir organização na API:', error);
      Alert.alert('Erro', 'Falha ao excluir organização. Tente novamente.');
    }
  }, []);

  const getOrganizationStats = useCallback(() => {
    const totalBooks = organizations.reduce((sum, org) => sum + org.stats.totalBooks, 0);
    const totalMembers = organizations.reduce((sum, org) => sum + org.stats.totalMembers, 0);
    const activeLoans = organizations.reduce((sum, org) => sum + org.stats.activeLoans, 0);
    const monthlyLoans = organizations.reduce((sum, org) => sum + org.stats.monthlyLoans, 0);

    return {
      totalBooks,
      totalMembers,
      activeLoans,
      monthlyLoans,
      totalOrganizations: organizations.length,
    };
  }, [organizations]);

  const getOrganizationById = useCallback((orgId: number) => {
    return organizations.find(org => org.id === orgId) || null;
  }, [organizations]);

  const getOrganizationsByUser = useCallback((userId: number) => {
    // Simular organizações do usuário baseado no ID
    return organizations.filter(org => {
      // Lógica simulada: usuário participa de organizações baseado no seu ID
      const userOrgIds = [1, 2, 3]; // IDs das organizações que o usuário participa
      return userOrgIds.includes(org.id);
    });
  }, [organizations]);

  return {
    // State
    organizations,
    selectedOrganization,
    isLoading,
    
    // Setters
    setSelectedOrganization,
    
    // Computed
    organizationStats: getOrganizationStats(),
    
    // Actions
    createOrganization,
    updateOrganization,
    deleteOrganization,
    getOrganizationById,
    getOrganizationsByUser,
  };
}; 