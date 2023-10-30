export type Json =
  | string
  | number
  | boolean
  | null
  | { [key: string]: Json }
  | Json[];

export type Database = {
  public: {
    Tables: {
      organization: {
        Row: {
          organizationId: string;
          name: string;
          ownerId: string | null;
        };
        Insert: {
          organizationId: string;
          name: string;
          ownerId: string | null;
        };
        Update: {
          organization_id: string;
          name: string;
          ownerId: string | null;
        };
      };
    };
    Views: {
      [_ in never]: never
    };
    Functions: {
      [_ in never]: never
    };
    Enums: {
      [_ in never]: never
    };
    CompositeTypes: {
      [_ in never]: never
    };
  };
};
