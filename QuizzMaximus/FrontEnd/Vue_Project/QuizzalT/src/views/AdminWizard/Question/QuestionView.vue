<template>
    <NavBarComponent></NavBarComponent>
    <component :is="componentName" @change-page="(name : string) => componentName = name"></component>
</template>

<script lang="ts">
import {QuestionIndexComponent, QuestionCreateComponent, QuestionDetailsComponent,
QuestionEditComponent, QuestionDeleteComponent} from '@/components/AdminRelated/QuestionComponents'

export default{
    data(){
        return {
            componentName: 'QuestionIndexComponent'
        }
    },
    components: {
        QuestionIndexComponent,
        QuestionCreateComponent,
        QuestionDetailsComponent,
        QuestionEditComponent,
        QuestionDeleteComponent
    }
}
</script>