import api from '../config/api';
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
    const response = await api.get<ExtendedOrganization[]>('/organizations');
    return response.data;
  }

  async getOrganizationById(organizationId: number): Promise<ExtendedOrganization> {
    const response = await api.get<ExtendedOrganization>(`/organizations/${organizationId}`);
    return response.data;
  }

  async createOrganization(orgData: CreateOrganizationRequest): Promise<ExtendedOrganization> {
    const response = await api.post<ExtendedOrganization>('/organizations', orgData);
    return response.data;
  }

  async updateOrganization(orgData: UpdateOrganizationRequest): Promise<ExtendedOrganization> {
    const { organizationId, ...data } = orgData;
    const response = await api.put<ExtendedOrganization>(`/organizations/${organizationId}`, data);
    return response.data;
  }

  async deleteOrganization(organizationId: number): Promise<void> {
    await api.delete(`/organizations/${organizationId}`);
  }

  async joinOrganization(organizationId: number): Promise<void> {
    await api.post(`/organizations/${organizationId}/join`);
  }

  async leaveOrganization(organizationId: number): Promise<void> {
    await api.post(`/organizations/${organizationId}/leave`);
  }

  async getOrganizationMembers(organizationId: number): Promise<any[]> {
    const response = await api.get(`/organizations/${organizationId}/members`);
    return response.data;
  }
}

export default new OrganizationService(); 