import antfu from "@antfu/eslint-config";

export default antfu({
  rules: {
    "style/semi": ["error", "always"],
    "style/quotes": ["error", "double", { allowTemplateLiterals: true, avoidEscape: true }],
  },
},
);
