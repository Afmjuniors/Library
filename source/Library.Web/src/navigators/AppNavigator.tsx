import React from 'react';
import { createBottomTabNavigator } from '@react-navigation/bottom-tabs';
import { createStackNavigator } from '@react-navigation/stack';

export type AppTabParamList = {
  Home: undefined;
  Books: undefined;
  Notifications: undefined;
  Organizations: undefined;
  Profile: undefined;
};

export type BookStackParamList = {
  BookList: undefined;
  BookDetail: { bookId: number };
  BookEdit: { bookId: number };
};

const Tab = createBottomTabNavigator<AppTabParamList>();
const Stack = createStackNavigator<BookStackParamList>();

// Placeholder components para evitar erros de navegação
const PlaceholderScreen: React.FC = () => {
  return null;
};

const BookStack: React.FC = () => {
  return (
    <Stack.Navigator screenOptions={{ headerShown: false }}>
      <Stack.Screen name="BookList" component={PlaceholderScreen} />
      <Stack.Screen name="BookDetail" component={PlaceholderScreen} />
      {/* Adicionar BookEditScreen quando implementado */}
    </Stack.Navigator>
  );
};

export const AppNavigator: React.FC = () => {
  return (
    <Tab.Navigator
      screenOptions={{
        headerShown: false,
        tabBarStyle: {
          backgroundColor: '#fff',
          borderTopWidth: 1,
          borderTopColor: '#e0e0e0',
        },
      }}
    >
      <Tab.Screen 
        name="Home" 
        component={PlaceholderScreen}
        options={{
          tabBarLabel: '📚 Livros',
          tabBarIcon: () => null,
        }}
      />
      <Tab.Screen 
        name="Notifications" 
        component={PlaceholderScreen}
        options={{
          tabBarLabel: '🔔 Notificações',
          tabBarIcon: () => null,
        }}
      />
      <Tab.Screen 
        name="Organizations" 
        component={PlaceholderScreen}
        options={{
          tabBarLabel: '🏢 Org',
          tabBarIcon: () => null,
        }}
      />
      <Tab.Screen 
        name="Profile" 
        component={PlaceholderScreen}
        options={{
          tabBarLabel: '👤 Perfil',
          tabBarIcon: () => null,
        }}
      />
    </Tab.Navigator>
  );
}; 