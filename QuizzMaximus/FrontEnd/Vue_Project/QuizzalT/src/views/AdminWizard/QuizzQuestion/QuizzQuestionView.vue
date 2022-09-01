<template>
    <NavBarComponent></NavBarComponent>
    <component :is="componentName" @change-page="(name : string) => componentName = name"></component>
</template>

<script lang="ts">
import { QuizzQuestionCreateComponent, QuizzQuestionDeleteComponent, QuizzQuestionEditComponent,
QuizzQuestionDetailsComponent, QuizzQuestionIndexComponent } from "@/components/AdminRelated/QuizzQuestionComponents"

export default{
    data(){
        return {
            componentName: 'QuizzQuestionIndexComponent'
        }
    },
    components: {
        QuizzQuestionCreateComponent,
        QuizzQuestionDeleteComponent,
        QuizzQuestionEditComponent,
        QuizzQuestionDetailsComponent,
        QuizzQuestionIndexComponent
    }
}
</script>