<template>
    <NavBarComponent></NavBarComponent>
    <component :is="componentName" @change-page="(name : string) => componentName = name"></component>
</template>

<script lang="ts">
import { PlayedQuestionIndexComponent, PlayedQuestionCreateComponent, PlayedQuestionDetailsComponent,
PlayedQuestionEditComponent, PlayedQuestionDeleteComponent} from '@/components/AdminRelated/PlayedQuestionComponents'

export default{
    data(){
        return {
            componentName: 'PlayedQuestionIndexComponent'
        }
    },
    components: {
        PlayedQuestionIndexComponent,
        PlayedQuestionCreateComponent,
        PlayedQuestionDetailsComponent,
        PlayedQuestionEditComponent,
        PlayedQuestionDeleteComponent
    }
}
</script>