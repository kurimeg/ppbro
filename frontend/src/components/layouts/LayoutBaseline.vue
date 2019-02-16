<template>
  <div>
    <app-header />
    <v-content>
      <slot />
      <app-fab />
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
  }
}
</script>
