<template>
    <NavBarComponent></NavBarComponent>
    <component :is="componentName" @change-page="(name : string) => componentName = name"></component>
</template>

<script lang="ts">
import { ThemeCreateComponent, ThemeDeleteComponent, ThemeDetailsComponent,
ThemeEditComponent, ThemeIndexComponent } from "@/components/AdminRelated/ThemeComponents"

export default{
    data(){
        return {
            componentName: 'ThemeIndexComponent'
        }
    },
    components: {
        ThemeCreateComponent,
        ThemeDeleteComponent,
        ThemeDetailsComponent,
        ThemeEditComponent,
        ThemeIndexComponent
    }
}
</script>