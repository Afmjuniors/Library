import { apiClient } from './api';
import { Organization, ExtendedOrganization, OrganizationRules } from '../types';

export interface CreateOrganizationRequest {
  name: string;
  description: string;
  image?: string;
  rules: OrganizationRules;
}

export interface UpdateOrganizationRequest extends Partial<CreateOrganizationRequest> {
  organizationId: number;
}

class OrganizationService {
  async getOrganizations(): Promise<ExtendedOrganization[]> {
    return await apiClient.get<ExtendedOrganization[]>('/organizations');
  }

  async getOrganizationById(organizationId: number): Promise<ExtendedOrganization> {
    return await apiClient.get<ExtendedOrganization>(`/organizations/${organizationId}`);
  }

  async createOrganization(orgData: CreateOrganizationRequest): Promise<ExtendedOrganization> {
    return await apiClient.post<ExtendedOrganization>('/organizations', orgData);
  }

  async updateOrganization(orgData: UpdateOrganizationRequest): Promise<ExtendedOrganization> {
    const { organizationId, ...data } = orgData;
    return await apiClient.put<ExtendedOrganization>(`/organizations/${organizationId}`, data);
  }

  async deleteOrganization(organizationId: number): Promise<void> {
    await apiClient.delete(`/organizations/${organizationId}`);
  }

  async joinOrganization(organizationId: number): Promise<void> {
    await apiClient.post(`/organizations/${organizationId}`);
  }

  async leaveOrganization(organizationId: number): Promise<void> {
    await apiClient.post(`/organizations/${organizationId}`);
  }

  async getOrganizationMembers(organizationId: number): Promise<any[]> {
    return await apiClient.get(`/organizations/${organizationId}/members`);
  }
}

export default new OrganizationService(); 