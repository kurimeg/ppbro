<template>
  <div>
    <app-header 
      :headerName="headerName"
    />
    <v-content>
      <slot />
      <app-fab v-if="isUser"/>
    </v-content>
  </div>
</template>

<script>
import AppHeader from '@/components/globals/AppHeader'
import AppFab from '@/components/globals/AppFab'

export default {
  components: {
    AppHeader,
    AppFab
  },
  props: {
    headerName: String,
    isUser: Boolean
  }
}
</script>
