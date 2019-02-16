<template>
  <v-speed-dial
    fixed
    bottom
    right
    v-model="fab"
  >
    <v-btn
      slot="activator"
      v-model="fab"
      color="primary"
      dark
      fab
    >
      <v-icon>account_circle</v-icon>
      <v-icon>close</v-icon>
    </v-btn>
    <sharer-selecter />
    <portfolio-form />
  </v-speed-dial>
</template>

<script>
import PortfolioForm from '@/pages/PortfolioForm'
import SharerSelecter from '@/components/SharerSelecter'

export default {
  components: {
    PortfolioForm,
    SharerSelecter
  },
  data: () => ({
    fab: false
  })
}
</script>

<style>

</style>
