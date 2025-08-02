import { useState, useEffect } from 'react';
import organizationService from '../services/organizationService';
import { ExtendedOrganization } from '../types';

export const useOrganizations = () => {
  const [organizations, setOrganizations] = useState<ExtendedOrganization[]>([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const fetchOrganizations = async () => {
    try {
      setLoading(true);
      setError(null);
      const fetchedOrganizations = await organizationService.getOrganizations();
      setOrganizations(fetchedOrganizations);
    } catch (err: any) {
      setError(err.response?.data?.message || 'Failed to fetch organizations');
      throw err;
    } finally {
      setLoading(false);
    }
  };

  const createOrganization = async (orgData: any) => {
    try {
      setError(null);
      const newOrganization = await organizationService.createOrganization(orgData);
      setOrganizations(prev => [...prev, newOrganization]);
      return newOrganization;
    } catch (err: any) {
      setError(err.response?.data?.message || 'Failed to create organization');
      throw err;
    }
  };

  const updateOrganization = async (orgData: any) => {
    try {
      setError(null);
      const updatedOrganization = await organizationService.updateOrganization(orgData);
      setOrganizations(prev => prev.map(org => 
        org.id === updatedOrganization.id ? updatedOrganization : org
      ));
      return updatedOrganization;
    } catch (err: any) {
      setError(err.response?.data?.message || 'Failed to update organization');
      throw err;
    }
  };

  const deleteOrganization = async (organizationId: number) => {
    try {
      setError(null);
      await organizationService.deleteOrganization(organizationId);
      setOrganizations(prev => prev.filter(org => org.id !== organizationId));
    } catch (err: any) {
      setError(err.response?.data?.message || 'Failed to delete organization');
      throw err;
    }
  };

  const joinOrganization = async (organizationId: number) => {
    try {
      setError(null);
      await organizationService.joinOrganization(organizationId);
    } catch (err: any) {
      setError(err.response?.data?.message || 'Failed to join organization');
      throw err;
    }
  };

  const leaveOrganization = async (organizationId: number) => {
    try {
      setError(null);
      await organizationService.leaveOrganization(organizationId);
    } catch (err: any) {
      setError(err.response?.data?.message || 'Failed to leave organization');
      throw err;
    }
  };

  return {
    organizations,
    loading,
    error,
    fetchOrganizations,
    createOrganization,
    updateOrganization,
    deleteOrganization,
    joinOrganization,
    leaveOrganization,
  };
}; 