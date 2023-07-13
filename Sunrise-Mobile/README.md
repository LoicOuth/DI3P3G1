# Mobile app

Run project
```bash
npm run start
```

# Explain folder structure

All application is conatin in source folder (src)

## components folder

This folder contain all components of the application. If a component is used in multiple screens inside the application so put it in the general folder.
If a component is only used in one screen so you can create a folder for the screen in the root of the folder components and you can put the component inside this folder 

Each component is a folder that contain a index.tsx and a style.ts. A component need to start with an uppercase and need to be a CamelCase

Example button component :

```
📂 src 
   |-> 📂 components
         |-> 📂 Button
               |-> 📄 index.tsx
               |-> 📄 style.ts
```

## core folder

This folder contain all models, interfaces, constants for all application. 
 - A model go in a subfolder models and is name is "***.model.ts"
 - A type go in a subfolder types and is name is "***.type.ts" (Use type not interface)
 - A constant go in a subfolder constants and is name is "***.constant.ts"

## hooks folder

This folder contain all hooks for the application. You can seperate them in different folder.
Hooks is a piece of reusable code that you can use in every application.

## navigation 

This folder contain all navigation logic. It's always have a main.ts that is the enter point of the navigation and he choose where user need to be redirected. After you need to create one file by navigation layout.

## providers

This folder contain all context provider of the application. Using with react context

## screens folder

This folder contain all screens of the application. If a feature have multiple screens create a folder for this. A screens is a routed component, so you need to used the same syntax as a component. refet to components folder

## services folder

This folder contain all service/API call of the application. Create file for a group of call api. 

Example :

```
📂 src 
   |-> 📂 services
            |-> 📂 auth
            |-> 📂 utils
            |-> 📂 posts
```

## styles folder

This folder contain all styles of the application. Contain one index.ts that import/export all style of subfolder.Create one subfolder for each component or put it in generalStyles folder.

Example :
```
📂 src 
   |-> 📂 styles
            |-> 📄 index.ts
            |-> 📂 generalStyles
            |-> 📂 formStyles
            |-> 📂 buttonStyles
```
