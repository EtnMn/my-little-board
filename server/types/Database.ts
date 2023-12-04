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
			member: {
				Row: {
					memberId: string;
					organizationId: string | null;
					profileId: string;
				};
				Insert: {
					memberId?: string;
					organizationId?: string | null;
					profileId: string;
				};
				Update: {
					memberId?: string;
					organizationId?: string | null;
					profileId?: string;
				};
				Relationships: [
					{
						foreignKeyName: "memberOrganizationIdFkey";
						columns: ["organizationId"];
						isOneToOne: false;
						referencedRelation: "organization";
						referencedColumns: ["organizationId"];
					},
					{
						foreignKeyName: "memberProfileIdFkey";
						columns: ["profileId"];
						isOneToOne: false;
						referencedRelation: "getProfiles";
						referencedColumns: ["profileId"];
					},
					{
						foreignKeyName: "memberProfileIdFkey";
						columns: ["profileId"];
						isOneToOne: false;
						referencedRelation: "profile";
						referencedColumns: ["profileId"];
					},
				];
			};
			organization: {
				Row: {
					name: string;
					organizationId: string;
					ownerId: string | null;
				};
				Insert: {
					name: string;
					organizationId?: string;
					ownerId?: string | null;
				};
				Update: {
					name?: string;
					organizationId?: string;
					ownerId?: string | null;
				};
				Relationships: [
					{
						foreignKeyName: "organizationOwnerIdFkey";
						columns: ["ownerId"];
						isOneToOne: false;
						referencedRelation: "getProfiles";
						referencedColumns: ["profileId"];
					},
					{
						foreignKeyName: "organizationOwnerIdFkey";
						columns: ["ownerId"];
						isOneToOne: false;
						referencedRelation: "profile";
						referencedColumns: ["profileId"];
					},
				];
			};
			profile: {
				Row: {
					avatar: string;
					email: string;
					name: string;
					profileId: string;
				};
				Insert: {
					avatar?: string;
					email?: string;
					name?: string;
					profileId: string;
				};
				Update: {
					avatar?: string;
					email?: string;
					name?: string;
					profileId?: string;
				};
				Relationships: [
					{
						foreignKeyName: "profileProfileIdFkey";
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
					role: string | null;
				};
				Relationships: [
					{
						foreignKeyName: "profileProfileIdFkey";
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

export type Tables<
	PublicTableNameOrOptions extends
	| keyof (Database["public"]["Tables"] & Database["public"]["Views"])
	| { schema: keyof Database },
	TableName extends PublicTableNameOrOptions extends { schema: keyof Database }
		? keyof (Database[PublicTableNameOrOptions["schema"]]["Tables"] &
		Database[PublicTableNameOrOptions["schema"]]["Views"])
		: never = never,
> = PublicTableNameOrOptions extends { schema: keyof Database }
	? (Database[PublicTableNameOrOptions["schema"]]["Tables"] &
	Database[PublicTableNameOrOptions["schema"]]["Views"])[TableName] extends {
		Row: infer R;
	}
		? R
		: never
	: PublicTableNameOrOptions extends keyof (Database["public"]["Tables"] &
	Database["public"]["Views"])
		? (Database["public"]["Tables"] &
		Database["public"]["Views"])[PublicTableNameOrOptions] extends {
			Row: infer R;
		}
			? R
			: never
		: never;

export type TablesInsert<
	PublicTableNameOrOptions extends
	| keyof Database["public"]["Tables"]
	| { schema: keyof Database },
	TableName extends PublicTableNameOrOptions extends { schema: keyof Database }
		? keyof Database[PublicTableNameOrOptions["schema"]]["Tables"]
		: never = never,
> = PublicTableNameOrOptions extends { schema: keyof Database }
	? Database[PublicTableNameOrOptions["schema"]]["Tables"][TableName] extends {
		Insert: infer I;
	}
		? I
		: never
	: PublicTableNameOrOptions extends keyof Database["public"]["Tables"]
		? Database["public"]["Tables"][PublicTableNameOrOptions] extends {
			Insert: infer I;
		}
			? I
			: never
		: never;

export type TablesUpdate<
	PublicTableNameOrOptions extends
	| keyof Database["public"]["Tables"]
	| { schema: keyof Database },
	TableName extends PublicTableNameOrOptions extends { schema: keyof Database }
		? keyof Database[PublicTableNameOrOptions["schema"]]["Tables"]
		: never = never,
> = PublicTableNameOrOptions extends { schema: keyof Database }
	? Database[PublicTableNameOrOptions["schema"]]["Tables"][TableName] extends {
		Update: infer U;
	}
		? U
		: never
	: PublicTableNameOrOptions extends keyof Database["public"]["Tables"]
		? Database["public"]["Tables"][PublicTableNameOrOptions] extends {
			Update: infer U;
		}
			? U
			: never
		: never;

export type Enums<
	PublicEnumNameOrOptions extends
	| keyof Database["public"]["Enums"]
	| { schema: keyof Database },
	EnumName extends PublicEnumNameOrOptions extends { schema: keyof Database }
		? keyof Database[PublicEnumNameOrOptions["schema"]]["Enums"]
		: never = never,
> = PublicEnumNameOrOptions extends { schema: keyof Database }
	? Database[PublicEnumNameOrOptions["schema"]]["Enums"][EnumName]
	: PublicEnumNameOrOptions extends keyof Database["public"]["Enums"]
		? Database["public"]["Enums"][PublicEnumNameOrOptions]
		: never;
