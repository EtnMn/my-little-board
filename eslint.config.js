import antfu from "@antfu/eslint-config";

export default antfu({
  rules: {
    "style/semi": ["error", "always"],
    "style/quotes": ["error", "double", { allowTemplateLiterals: true, avoidEscape: true }],
    "style/member-delimiter-style": ["error", { multiline: { delimiter: "semi" } }],
    "ts/consistent-type-definitions": ["error", "type"],
  },
},
);
