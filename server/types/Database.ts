import type { UserRole } from "~/types";

export type Json =
  | string
  | number
  | boolean
  | null
  | { [key: string]: Json | undefined }
  | Json[];

export type Database = {
  public: {
    Tables: {
      profile: {
        Row: {
          avatar: string;
          createdAt: string;
          email: string;
          name: string;
          profileId: string;
        };
        Insert: {
          avatar?: string;
          createdAt?: string;
          email?: string;
          name?: string;
          profileId: string;
        };
        Update: {
          avatar?: string;
          createdAt?: string;
          email?: string;
          name?: string;
          profileId?: string;
        };
        Relationships: [
          {
            foreignKeyName: "profile_profileId_fkey";
            columns: ["profileId"];
            isOneToOne: true;
            referencedRelation: "users";
            referencedColumns: ["id"];
          },
        ];
      };
    };
    Views: {
      getProfiles: {
        Row: {
          avatar: string;
          email: string;
          name: string;
          profileId: string;
          role?: UserRole;
        };
        Relationships: [
          {
            foreignKeyName: "profile_profileId_fkey";
            columns: ["profileId"];
            isOneToOne: true;
            referencedRelation: "users";
            referencedColumns: ["id"];
          },
        ];
      };
    };
    Functions: {
      delete_claim: {
        Args: {
          uid: string;
          claim: string;
        };
        Returns: string;
      };
      get_claim: {
        Args: {
          uid: string;
          claim: string;
        };
        Returns: Json;
      };
      get_claims: {
        Args: {
          uid: string;
        };
        Returns: Json;
      };
      get_my_claim: {
        Args: {
          claim: string;
        };
        Returns: Json;
      };
      get_my_claims: {
        Args: Record<PropertyKey, never>;
        Returns: Json;
      };
      is_claims_admin: {
        Args: Record<PropertyKey, never>;
        Returns: boolean;
      };
      set_claim: {
        Args: {
          uid: string;
          claim: string;
          value: Json;
        };
        Returns: string;
      };
    };
    Enums: {
      [_ in never]: never
    };
    CompositeTypes: {
      [_ in never]: never
    };
  };
};
