<template>
    <NavBarComponent></NavBarComponent>
    <component :is="componentName" @change-page="(name : string) => componentName = name"></component>
</template>

<script lang="ts">
import { QuizzCreateComponent, QuizzDeleteComponent, QuizzDetailsComponent,
QuizzEditComponent, QuizzIndexComponent } from "@/components/AdminRelated/QuizzComponents"
export default{
    data(){
        return {
            componentName: 'QuizzIndexComponent'
        }
    },
    components: {
        QuizzCreateComponent,
        QuizzDeleteComponent,
        QuizzDetailsComponent,
        QuizzEditComponent,
        QuizzIndexComponent
    }
}
</script>